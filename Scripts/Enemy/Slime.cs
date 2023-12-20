using System.Collections;
using UnityEngine;

public class Slime : Enemy
{
    private PlayerSkillManager player;
    private Animator animator;
    private EnemyHealth health;
    [SerializeField] private float minDamage;
    [SerializeField] private float maxDamage;
    [SerializeField] private int prizeKeyAmount;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = CombatManager.Instance.GetPlayer();
        playerHealth = player.GetComponent<PlayerHealth>();
        health = GetComponent<EnemyHealth>();

        CombatManager.Instance.OnEnemyTurn += Attack;
        health.OnDie += OnDie;

        currnetMana = lastTurnMana;
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
        StartCoroutine(AttackC());
    }

    private IEnumerator AttackC()
    {
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(1);
        basicAttackDamage = GetRandomDamage();
        playerHealth.TakeDamage(new BasicAttack(playerHealth.GetComponent<Resistences>(), basicAttackDamage));
        yield return new WaitForSeconds(1);
        StartPlayerTurn();
    }

    private void StartPlayerTurn()
    {
        CombatManager.Instance.StartPlayerTurn();
    }

    private float GetRandomDamage()
    {
        float damage = Random.Range(minDamage, maxDamage);
        return damage;
    }
    private void OnDestroy()
    {
        CombatManager.Instance.OnEnemyTurn -= Attack;
    }
}



