public class TrueDamage : DamageType
{
    public TrueDamage(Resistences resistence, float damage)
    {
        this.resistence = resistence;
        this.damage = damage;
    }
    public override float CalculateDamage()
    {
        return damage;
    }
}
