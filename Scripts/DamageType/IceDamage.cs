
public class IceDamage : DamageType
{
    public IceDamage(Resistences resistence, float damage)
    {
        this.resistence = resistence;
        this.damage = damage;
    }
    public override float CalculateDamage()
    {
        float resistancePercentage = resistence.GetIceResistence();
        float resistentAmount = damage * (resistancePercentage / 100f);
        float result = damage - resistentAmount;
        return result;
    }
}
