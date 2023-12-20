using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatUIManager : MonoBehaviour
{
    public static CombatUIManager Instance;
    public Canvas CombatCanvas;
    private List<SkillIcon> skillIcons = new List<SkillIcon>();
    [SerializeField] private GameObject skillPanel;
    [SerializeField] private CombatManager combatManager;
    [SerializeField] private PlayerSkillManager skillManager;
    [SerializeField] private GameObject skillIconPrefab;
    [Header("Basic Attack")]
    [SerializeField] private GameObject attackBlockImage;
    [SerializeField] private PlayerAttack playerAttack;
    [Header("Mana")]
    [SerializeField] private TMP_Text manaText;
    [SerializeField] private TMP_Text lastTurnManaText;
    [Header("TurnChange")]
    [SerializeField] private Button turnChangeButton;
    [SerializeField] private GameObject lockImage;
    [Header("Deck")]
    [SerializeField] private TMP_Text remainingSkillText;
    [Header("Impacts")]
    [SerializeField] private GameObject impactIcon;
    [SerializeField] private Transform playerBuff;
    [SerializeField] private Transform playerDebuff;
    private Transform enemyBuff;
    private Transform enemyDebuff;
    private Dictionary<Impact, GameObject> activeImpacts = new Dictionary<Impact, GameObject>();

    private void Awake()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        #endregion

        combatManager.OnCombatStart += OpenCombatUI;
        combatManager.OnEnemyDied += CloseCombatUI;
        combatManager.OnCombatEnding += ResetUI;
        combatManager.OnCombatStart += SetEnemyImpactContents;
        combatManager.OnPlayerTurn += DeactivateAttackBlockImage;
        combatManager.OnCombatEnding += DeactivateAttackBlockImage;
        combatManager.OnPlayerTurn += ActivateTurnChangeButton;
        combatManager.OnEnemyTurn += DeactivateTurnChangeButton;
        skillManager.OnSkillAdded += OnSkillAddded;
        playerAttack.OnAttack += ActivateAttackBlockImage;
        playerAttack.OnAttack += DeactivateTurnChangeButton;
        playerAttack.OnAttackEnd += ActivateTurnChangeButton;

        skillManager.OnCurrnetManaChanged += ChangeManaText;
        skillManager.OnLastTurnManaChanged += ChangeLastTurnManaText;
        skillManager.OnDeckDecrease += ChangeRemainingSkillText;
        skillManager.OnSkillUsageStart += DeactivateTurnChangeButton;
        skillManager.OnSkillUsageEnd += ActivateTurnChangeButton;
    }
    public void OpenCombatUI()
    {
        skillPanel.SetActive(true);
    }
    public void CloseCombatUI()
    {
        skillPanel.SetActive(false);
    }
    public void CreateImpactIcon(Impact impact, GameObject impactdescription, ImpactType type)
    {
        Transform target = GetImpactContent(type);
        GameObject prefab = Instantiate(impactIcon, target);
        ImpactIcon icon = prefab.GetComponent<ImpactIcon>();
        GameObject description = Instantiate(impactdescription, prefab.transform);
        icon.Initialize(impact.impactSprite, description);
        impact.SetUI(description);
        activeImpacts.Add(impact, prefab);
    }
    public void RemoveImpactIcon(Impact impact)
    {
        GameObject icon;

        if (activeImpacts.TryGetValue(impact, out icon))
        {
            activeImpacts.Remove(impact);
            Destroy(icon);
        }
    }
    public void RemoveSkillIcon(SkillIcon icon)
    {
        skillIcons.Remove(icon);
    }

    private void OnSkillAddded(Skill skill)
    {
        SkillSlot slot = skillManager.GetFirstEmptySlot();
        slot.AddSkill(skill);
        GameObject go = Instantiate(skillIconPrefab, slot.transform);
        SkillIcon icon = go.GetComponent<SkillIcon>();
        icon.Initialize(slot, skill.SkillIcon, skill.RequiredMana, this);
        GameObject skillDescription = Instantiate(skill.SkillDescription, icon.transform);
        icon.SkillDescription = skillDescription;
        RectTransform rectTransform = go.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = Vector3.zero;
        skillIcons.Add(icon);
    }
    private void ActivateAttackBlockImage()
    {
        attackBlockImage.SetActive(true);
    }
    private void DeactivateAttackBlockImage()
    {
        attackBlockImage.SetActive(false);
    }
    private void ChangeManaText(int mana)
    {
        manaText.text = mana.ToString();
    }
    private void ChangeLastTurnManaText(int value)
    {
        lastTurnManaText.text = value.ToString();
    }
    private void DeactivateTurnChangeButton()
    {
        turnChangeButton.enabled = false;
        lockImage.SetActive(true);
    }
    private void ActivateTurnChangeButton()
    {
        turnChangeButton.enabled = true;
        lockImage.SetActive(false);
    }
    private void ChangeRemainingSkillText(int value)
    {
        remainingSkillText.text = value.ToString();
    }
    private void SetEnemyImpactContents()
    {
        enemyBuff = combatManager.GetEnemy().BuffContent;
        enemyDebuff = combatManager.GetEnemy().DebuffContent;
    }
    private Transform GetImpactContent(ImpactType type)
    {
        switch (type)
        {
            case ImpactType.PlayerBuff:
                return playerBuff;
            case ImpactType.PlayerDebuff:
                return playerDebuff;
            case ImpactType.EnemyBuff:
                return enemyBuff;
            case ImpactType.EnemyDebuff:
                return enemyDebuff;
            default: return playerBuff;
        }
    }
    private void ResetUI()
    {
        for (int i = 0; i < skillIcons.Count; i++)
        {
            SkillIcon icon = skillIcons[i];
            Destroy(icon.gameObject);
        }

        for (int i = 0; i < activeImpacts.Count; i++)
        {
            var element = activeImpacts.ElementAt(i);
            GameObject target = element.Value;
            Destroy(target);
        }

        activeImpacts.Clear();
        skillIcons.Clear();
        ActivateTurnChangeButton();
        ActivateAttackBlockImage();
    }
}


public enum ImpactType
{
    PlayerBuff, PlayerDebuff, EnemyBuff, EnemyDebuff
}
