using System;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public Transform BuffContent;
    public Transform DebuffContent;
    public event Action<int, int> OnManaChanged;
    protected int currnetMana;
    [SerializeField] protected int maxMana;
    [SerializeField] protected int lastTurnMana;
    [SerializeField] protected int basicAttackManaConsume;
    [SerializeField] protected int defaultBasicAttackDamage;
    protected float basicAttackDamage;
    protected PlayerHealth playerHealth;
    public virtual void ManaChanged()
    {
        OnManaChanged?.Invoke(currnetMana, maxMana);
    }

    public abstract void OnDie();
    public void ResetBasicAttackDamage()
    {
        basicAttackDamage = defaultBasicAttackDamage;
    }
    public void IncreaseAttackDamage(float damage)
    {
        basicAttackDamage += damage;
    }
}
