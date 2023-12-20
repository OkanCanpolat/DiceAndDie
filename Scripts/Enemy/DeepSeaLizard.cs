using System.Collections;
using UnityEngine;
public class DeepSeaLizard : Enemy, IStunable
{
    private PlayerSkillManager player;
    private Animator enemyAnimator;
    private ImpactManager playerImpacts;
    private Animator animator;
    private EnemyHealth health;
    [SerializeField] private Skill waterSkill;
    [SerializeField] private Skill electricSkill;
    [SerializeField] private Skill healSkill;
    [SerializeField] private int prizeKeyAmount;
    private bool healed;
    public bool IsStunned { get; set; }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = CombatManager.Instance.GetPlayer();
        playerHealth = player.GetComponent<PlayerHealth>();
        playerImpacts = player.GetComponent<PlayerImpactManager>();
        enemyAnimator = GetComponent<Animator>();
        health = GetComponent<EnemyHealth>();

        CombatManager.Instance.OnPlayerTurn += IncreaseMana;
        CombatManager.Instance.OnEnemyTurn += Attack;
        health.OnDie += OnDie;

        currnetMana = lastTurnMana;
        basicAttackDamage = defaultBasicAttackDamage;
    }

    private void Start()
    {
        ManaChanged();
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
        yield return StartCoroutine(ControlHealSkill());
        yield return StartCoroutine(ControlWaterSkill());
        yield return StartCoroutine(ControlElectricSkill());
        yield return StartCoroutine(AttackC());
    }
    private IEnumerator AttackC()
    {
        if(currnetMana >= basicAttackManaConsume)
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
    private IEnumerator ControlWaterSkill()
    {
        bool playerIsWet = playerImpacts.HasPassiveImpact(typeof(WetImpact));

        if (!playerIsWet && waterSkill.RequiredMana <= currnetMana)
        {
            waterSkill.Use();
            currnetMana -= waterSkill.RequiredMana;
            ManaChanged();
            animator.SetTrigger(waterSkill.animationParameterName);
            yield return new WaitForSeconds(waterSkill.skillCompleteTime);
        }
    }
    private IEnumerator ControlElectricSkill()
    {
        bool playerIsWet = playerImpacts.HasPassiveImpact(typeof(WetImpact));

        if (playerIsWet && electricSkill.RequiredMana <= currnetMana)
        {
            electricSkill.Use();
            currnetMana -= electricSkill.RequiredMana;
            ManaChanged();
            animator.SetTrigger(electricSkill.animationParameterName);
            yield return new WaitForSeconds(electricSkill.skillCompleteTime);
        }
    }
    private IEnumerator ControlHealSkill()
    {
        float maxHealth = health.GetMaxHealth();
        float currenctHealth = health.GetCurrentHealth();
        if(currenctHealth <= maxHealth/2 && !healed && currnetMana >= healSkill.RequiredMana)
        {
            healSkill.Use();
            currnetMana -= healSkill.RequiredMana;
            ManaChanged();
            animator.SetTrigger(electricSkill.animationParameterName);
            healed = true;
            yield return new WaitForSeconds(healSkill.skillCompleteTime);
        }
    }
    private void StartPlayerTurn()
    {
        CombatManager.Instance.StartPlayerTurn();
    }
    private void IncreaseMana()
    {
        lastTurnMana++;
        currnetMana = lastTurnMana;
        currnetMana = Mathf.Clamp(currnetMana, 0, maxMana);
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
    private void OnDestroy()
    {
        CombatManager.Instance.OnEnemyTurn -= Attack;
        CombatManager.Instance.OnPlayerTurn -= IncreaseMana;

    }
}
