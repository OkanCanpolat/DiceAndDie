using UnityEngine;

public class EnemyHealth : Health
{
    public bool isDead;
    private void Awake()
    {
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
        }
    }
    public void RestoreHealth(float value)
    {
        currentHealth += value;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        HealthChanged();
    }
}
