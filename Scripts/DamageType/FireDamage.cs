public class FireDamage : DamageType
{
    public FireDamage(Resistences resistence, float damage)
    {
        this.resistence = resistence;
        this.damage = damage;
    }
    public override float CalculateDamage()
    {
        float resistancePercentage = resistence.GetFireResistence();
        float resistentAmount = damage * (resistancePercentage / 100f);
        float result = damage - resistentAmount;
        return result;
    }
}
