using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalDamage : DamageType
{
    public PhysicalDamage(Resistences resistence, float damage)
    {
        this.resistence = resistence;
        this.damage = damage;
    }
    public override float CalculateDamage()
    {
        float resistancePercentage = resistence.GetPhysicalResistence();
        float resistentAmount = damage * (resistancePercentage / 100f);
        float result = damage - resistentAmount;
        return result;
    }
}
