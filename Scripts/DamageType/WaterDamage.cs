
public class WaterDamage : DamageType
{
    public WaterDamage(Resistences resistence, float damage)
    {
        this.resistence = resistence;
        this.damage = damage;
    }
    public override float CalculateDamage()
    {
        float resistancePercentage = resistence.GetWaterResistence();
        float resistentAmount = damage * (resistancePercentage / 100f);
        float result = damage - resistentAmount;
        return result;
    }
}
