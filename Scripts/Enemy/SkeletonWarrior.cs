using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWarrior : Enemy, IStunable
{
    private PlayerSkillManager player;
    private Animator enemyAnimator;
    private Animator animator;
    private EnemyHealth health;

    [SerializeField] private int prizeKeyAmount;
    [SerializeField] private AllDamageReducerBuff deffenseBuff;
    private int deffenseBuffTurnDelay = 2;
    private int deffenseBuffTurnCount = 0;
    [SerializeField] private BasicAttackDamageEnhancer damageBonusBuff;
    private int damageBuffTurnCount = 0;
    private int damageBuffTurnDelay = 2;
    [SerializeField] private ShieldBash shieldBash;
    private int stunSkillDelay = 4;
    private int stunSkillTurnCount;
    [SerializeField] private SkullBleed bleedSkill;
    private int bleedDelay = 3;
    private int bleedTurnCount = 3;

    public bool IsStunned { get; set; }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = CombatManager.Instance.GetPlayer();
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyAnimator = GetComponent<Animator>();
        health = GetComponent<EnemyHealth>();

        CombatManager.Instance.OnEnemyTurn += ReduceTurnCounts;
        CombatManager.Instance.OnEnemyTurn += Attack;
        CombatManager.Instance.OnPlayerTurn += IncreaseMana;
        health.OnDie += OnDie;

        currnetMana = lastTurnMana;
        basicAttackDamage = defaultBasicAttackDamage;
    }

    private void Start()
    {
        ManaChanged();
    }
    public override void OnDie()
    {
        CombatManager.Instance.EnemyDied();
        animator.SetTrigger("Die");
    }
    public void Die()
    {
        player.GetComponent<PlayerStuffManager>().IncreaseKey(prizeKeyAmount);
        Destroy(gameObject, 1f);
        CombatManager.Instance.FinishCombat();
    }
    private void Attack()
    {
        if (health.isDead) return;
        if (IsStunned)
        {
            StartPlayerTurn();
            return;
        }
        StartCoroutine(TemplateMethod());
    }
    private IEnumerator TemplateMethod()
    {
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(ShieldBash());
        yield return StartCoroutine(ControlBleed());
        yield return StartCoroutine(ControlDeffenseBuff());
        yield return StartCoroutine(ControlAttackBuff());
        yield return StartCoroutine(AttackC());
    }
    private IEnumerator AttackC()
    {
        if (currnetMana >= basicAttackManaConsume)
        {
            enemyAnimator.SetTrigger("Attack");
            currnetMana -= basicAttackManaConsume;
            ManaChanged();
            yield return new WaitForSeconds(1);
            playerHealth.TakeDamage(new BasicAttack(playerHealth.GetComponent<Resistences>(), basicAttackDamage));
            yield return new WaitForSeconds(1);
            StartPlayerTurn();
        }

        else
        {
            StartPlayerTurn();
        }
    }
    private IEnumerator ControlDeffenseBuff()
    {
        if(deffenseBuffTurnCount == 0 && currnetMana >= deffenseBuff.RequiredMana)
        {
            deffenseBuffTurnCount = deffenseBuff.turnCount + deffenseBuffTurnDelay;
            deffenseBuff.Use();
            currnetMana -= deffenseBuff.RequiredMana;
            ManaChanged();
            animator.SetTrigger(deffenseBuff.animationParameterName);
            yield return new WaitForSeconds(deffenseBuff.skillCompleteTime);
        }
    }
    private IEnumerator ControlAttackBuff()
    {
        if (damageBuffTurnCount == 0 && currnetMana >= damageBonusBuff.RequiredMana + basicAttackManaConsume)
        {
            damageBuffTurnCount = damageBonusBuff.turnCount + damageBuffTurnDelay;
            damageBonusBuff.Use();
            currnetMana -= damageBonusBuff.RequiredMana;
            ManaChanged();
            animator.SetTrigger(damageBonusBuff.animationParameterName);
            yield return new WaitForSeconds(damageBonusBuff.skillCompleteTime);
        }
    }
    private IEnumerator ShieldBash()
    {
        if(stunSkillTurnCount <= 0 && currnetMana >= shieldBash.RequiredMana)
        {
            stunSkillTurnCount = stunSkillDelay;
            shieldBash.Use();
            currnetMana -= shieldBash.RequiredMana;
            ManaChanged();
            animator.SetTrigger(shieldBash.animationParameterName);
            yield return new WaitForSeconds(shieldBash.skillCompleteTime);
        }
    }
    private IEnumerator ControlBleed()
    {
        if(bleedTurnCount == 0 && currnetMana >= bleedSkill.RequiredMana)
        {
            bleedTurnCount = bleedDelay;
            bleedSkill.Use();
            currnetMana -= bleedSkill.RequiredMana;
            ManaChanged();
            animator.SetTrigger(bleedSkill.animationParameterName);
            yield return new WaitForSeconds(bleedSkill.skillCompleteTime);
        }
    }
   
    private void IncreaseMana()
    {
        lastTurnMana++;
        currnetMana = lastTurnMana;
        currnetMana = Mathf.Clamp(currnetMana, 0, maxMana);
        ManaChanged();
    }
    private void StartPlayerTurn()
    {
        CombatManager.Instance.StartPlayerTurn();
    }

    private void ReduceTurnCounts()
    {
        if(deffenseBuffTurnCount > 0)
        {
            deffenseBuffTurnCount--;
        }
        if (damageBuffTurnCount > 0)
        {
            damageBuffTurnCount--;
        }
        if(stunSkillTurnCount > 0)
        {
            stunSkillTurnCount--;
        }
        if(bleedTurnCount > 0)
        {
            bleedTurnCount--;
        }
    }

    private void OnDestroy()
    {
        CombatManager.Instance.OnEnemyTurn -= Attack;
        CombatManager.Instance.OnPlayerTurn -= IncreaseMana;
        CombatManager.Instance.OnEnemyTurn -= ReduceTurnCounts;
    }

}
