
public abstract class DamageType 
{
    protected Resistences resistence;
    protected float damage;
    public abstract float CalculateDamage();
    public virtual void SetDamage(float damage)
    {
        this.damage = damage;
    }
    public  float GetDamage()
    {
        return damage;
    }
}
