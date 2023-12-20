
using UnityEngine;

public class BasicAttack : DamageType
{
    public BasicAttack(Resistences resistence, float damage)
    {
        this.resistence = resistence;
        this.damage = damage;
    }
    public override float CalculateDamage()
    {
        float dodgeChange = resistence.GetDodgeChange();
        int value = Random.Range(0, 100);

        if(value < dodgeChange)
        {
            return 0;
        }

        float resistancePercentage = resistence.GetPhysicalResistence();
        float resistentAmount = damage * (resistancePercentage / 100f);
        float result = damage - resistentAmount;
        return result;
    }
}
