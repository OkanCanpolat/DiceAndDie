using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public class PlayerSkillManager : MonoBehaviour
{
    public event Action<Skill> OnSkillAdded;
    public event Action<int> OnCurrnetManaChanged;
    public event Action<int> OnLastTurnManaChanged;
    public event Action<int> OnDeckDecrease;
    public event Action OnSkillUsageStart;
    public event Action OnSkillUsageEnd;
    public List<SkillSlot> SkillSlots => slots;
    [SerializeField] private SkillSelectionSaveFile savedSkills;
    [SerializeField] private List<Skill> selectedSkills;
    [SerializeField] private List<SkillSlot> slots;
    [SerializeField] private CombatManager combatManager;
    [SerializeField] private List<Skill> combatTemporarySkills;
    private Animator playerAnimator;
    private List<Skill> useableSkills = new List<Skill>();
    private const int begginingSkillCount = 5;
    private int lastTurnManaCount;
    private int currentMana;
    private int startingManaCount = 2;
    private int maxMana = 10;
    private bool canCastSpeel = true;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        combatManager.OnCombatStart += SetTemporarySkills;
        combatManager.OnCombatStart += SetBeginnigSkils;
        combatManager.OnPlayerTurn += AddSkillToSlot;
        combatManager.OnPlayerTurn += IncreaseManaOnPlayerTurn;
        combatManager.OnPlayerTurn += SetSkillsUseable;
        combatManager.OnEnemyTurn += SetSkillsUnuseable;
        combatManager.OnCombatEnding += ResetSkillsOnCombatEnd;

        currentMana = startingManaCount;
        lastTurnManaCount = startingManaCount;
    }

    private void Start()
    {
        OnLastTurnManaChanged?.Invoke(lastTurnManaCount);
        OnCurrnetManaChanged?.Invoke(currentMana);
        SetSelectedSkills();
    }

    public int GetCurrentMana()
    {
        return currentMana;
    }
    private void SetBeginnigSkils()
    {
        for (int i = 0; i < begginingSkillCount; i++)
        {
            Skill randomSkill = GetRandomSkill();
            useableSkills.Add(randomSkill);
            combatTemporarySkills.Remove(randomSkill);
            OnSkillAdded?.Invoke(randomSkill);
        }

        OnDeckDecrease?.Invoke(combatTemporarySkills.Count);
    }
    private void SetTemporarySkills()
    {
        foreach (Skill skill in selectedSkills)
        {
            combatTemporarySkills.Add(skill);
        }
    }
    public void AddSkillToSlot()
    {
        if (combatTemporarySkills.Count > 0)
        {
            foreach (SkillSlot slot in slots)
            {
                if (slot.IsEmpty())
                {
                    Skill randomSkill = GetRandomSkill();
                    useableSkills.Add(randomSkill);
                    combatTemporarySkills.Remove(randomSkill);
                    OnSkillAdded?.Invoke(randomSkill);
                    OnDeckDecrease?.Invoke(combatTemporarySkills.Count);
                    return;
                }
            }
        }
    }
    private Skill GetRandomSkill()
    {
        int index = UnityEngine.Random.Range(0, combatTemporarySkills.Count);
        Skill randomSkill = combatTemporarySkills[index];
        return randomSkill;
    }
    public bool UseSkillIfValid(Skill skill)
    {
        if (currentMana >= skill.RequiredMana && canCastSpeel)
        {
            UseMana(skill.RequiredMana);
            skill.Use();
            StartCoroutine(PreventSkillUsage(skill.skillCompleteTime));
            playerAnimator.SetTrigger(skill.animationParameterName);
            ControlTurnState(skill);
            OnSkillUsageStart?.Invoke();
            return true;
        }
        return false;
    }
    public void IncreaseMana(int value)
    {
        currentMana += value;
        currentMana = Mathf.Clamp(currentMana, 0, maxMana);
        OnCurrnetManaChanged?.Invoke(currentMana);
    }
    public void SetLastTurnMana(int value)
    {
        lastTurnManaCount = value;
        OnLastTurnManaChanged?.Invoke(lastTurnManaCount);

        if(currentMana < lastTurnManaCount)
        {
            currentMana = lastTurnManaCount;
            OnCurrnetManaChanged?.Invoke(currentMana);
        }
    }
    public void IncreaseLastTurnManaCount(int value)
    {
        lastTurnManaCount += value;
        lastTurnManaCount = Mathf.Clamp(lastTurnManaCount, 0, maxMana);
        OnLastTurnManaChanged?.Invoke(lastTurnManaCount);
    }
    public int GetLastTurnMana()
    {
        return lastTurnManaCount;
    }
    public void ChangeStartingManaCount(int value)
    {
        int manaDifference = currentMana - startingManaCount;
        startingManaCount += value;
        startingManaCount = Mathf.Clamp(startingManaCount, 0, maxMana);
        lastTurnManaCount = startingManaCount;
        currentMana = startingManaCount + manaDifference;
        OnCurrnetManaChanged?.Invoke(currentMana);
        OnLastTurnManaChanged?.Invoke(lastTurnManaCount);
    }
    public void UseMana(int value)
    {
        currentMana -= value;
        currentMana = Mathf.Clamp(currentMana, 0, maxMana);
        OnCurrnetManaChanged?.Invoke(currentMana);
    }
    private void IncreaseManaOnPlayerTurn()
    {
        lastTurnManaCount++;
        lastTurnManaCount = Mathf.Clamp(lastTurnManaCount, 0, maxMana);
        currentMana = lastTurnManaCount;
        OnCurrnetManaChanged?.Invoke(currentMana);
        OnLastTurnManaChanged?.Invoke(lastTurnManaCount);
    }
    public SkillSlot GetFirstEmptySlot()
    {
        foreach (SkillSlot slot in slots)
        {
            if (slot.IsEmpty())
            {
                return slot;
            }
        }

        return null;
    }
    public void SetCanUseSpell(bool value)
    {
        canCastSpeel = value;
    }
    public bool CanUseSpell()
    {
        return canCastSpeel;
    }
    private void SetSkillsUseable()
    {
        canCastSpeel = true;
    }
    private void SetSkillsUnuseable()
    {
        canCastSpeel = false;
    }
    private void ControlTurnState(Skill skill)
    {
        if (skill.turnConsume)
        {
            combatManager.SetIsPlayerTurn(false);
            StartCoroutine(FinishTurn(skill.skillCompleteTime));
        }
    }
    private IEnumerator FinishTurn(float delay)
    {
        yield return new WaitForSeconds(delay);
        combatManager.StartEnemyTurn();
    }
    private IEnumerator PreventSkillUsage(float time)
    {
        canCastSpeel = false;
        yield return new WaitForSeconds(time);
        canCastSpeel = true;
        OnSkillUsageEnd?.Invoke();
    }
    private void ResetSkillsOnCombatEnd()
    {
        canCastSpeel = true;
        combatTemporarySkills.Clear();
        useableSkills.Clear();
        lastTurnManaCount = startingManaCount;
        currentMana = startingManaCount;
        OnCurrnetManaChanged?.Invoke(currentMana);
        OnLastTurnManaChanged?.Invoke(lastTurnManaCount);

        foreach (SkillSlot slot in slots)
        {
            slot.ClearSlot();
        }
    }
    private void SetSelectedSkills()
    {
        selectedSkills.Clear();

        foreach(Skill skill in savedSkills.SelectedSkills)
        {
            selectedSkills.Add(skill);
        }
    }
}
