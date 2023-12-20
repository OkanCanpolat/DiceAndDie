using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    public event Action OnDie;
    public event Action<float, float> OnHealthChanged;
    protected float currentHealth;
    [SerializeField] protected float maxHealth;
    protected ImpactManager impactManager;
    public abstract void TakeDamage(DamageType type);
    public virtual void HealthChanged()
    {
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }
    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public void IncreaseMaxHealth(float value)
    {
        maxHealth += value;
        HealthChanged();
    }
    public float GetMaxHealth()
    {
        return maxHealth;
    }
    public void UseDeffenceImpacts(DamageType type)
    {
        if (impactManager.GetDeffensiveýmpacts() == null) return;

        List<DeffensiveImpact> list = impactManager.GetDeffensiveýmpacts();

        for (int i = 0; i< list.Count; i++)
        {
            list[i].OnDeffense(type);
        }
    }
    public void OnDead()
    {
        OnDie?.Invoke();
    }
}
