using UnityEngine;

public class PlayerHealth : Health
{
    public bool isDead;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        impactManager = GetComponent<ImpactManager>();
        currentHealth = maxHealth;
    }

    private void Start()
    {
        HealthChanged();
    }
    public override void TakeDamage(DamageType type)
    {
        UseDeffenceImpacts(type);
        int damage = (int)type.CalculateDamage();
        if (damage <= 0) return;
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        HealthChanged();

        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            OnDead();
            animator.SetTrigger("Die");
            CombatManager.Instance.EnemyDied();
        }
    }
    
    public void RestoreHealth(float amount)
    {
        currentHealth += (int) amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        HealthChanged();
    }
}
