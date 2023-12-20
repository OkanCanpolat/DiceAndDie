public class BleedDamage : DamageType
{
    public BleedDamage(Resistences resistence, float damage)
    {
        this.resistence = resistence;
        this.damage = damage;
    }
    public override float CalculateDamage()
    {
        float resistancePercentage = resistence.GetBleedResistence();
        float resistentAmount = damage * (resistancePercentage / 100f);
        float result = damage - resistentAmount;
        return result;
    }
}
