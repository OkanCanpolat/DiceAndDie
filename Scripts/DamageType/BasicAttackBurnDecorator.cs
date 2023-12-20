using UnityEngine;

public class BasicAttackBurnDecorator : DamageType
{
    private float burnDamage;
    private float burnChance;
    private int burnCount;
    private float fireDamage;
    private ImpactManager impacts;
    private GameObject impactDescription;
    private Health health;
    private Sprite impactSprite;
    private DamageType fireType;

    public BasicAttackBurnDecorator(Resistences resistence, float damage, float burnDamage, float burnChance, int burnCount, float fireDamage)
    {
        this.resistence = resistence;
        this.damage = damage;
        this.burnChance = burnChance;
        this.burnDamage = burnDamage;
        this.burnCount = burnCount;
        this.fireDamage = fireDamage;
        fireType = new FireDamage(resistence, fireDamage);
    }
    public void Init(GameObject impactDescription, Health health, Sprite impactSprite)
    {
        this.impactDescription = impactDescription;
        this.health = health;
        this.impactSprite = impactSprite;
        impacts = resistence.GetComponent<ImpactManager>();
    }
    
    public override float CalculateDamage()
    {
        float dodgeChange = resistence.GetDodgeChange();
        int value = Random.Range(0, 100);

        if (value < dodgeChange)
        {
            return 0;
        }

        health.TakeDamage(fireType);
        ControlBurn();
        float resistancePercentage = resistence.GetPhysicalResistence();
        float resistentAmount = damage * (resistancePercentage / 100f);
        float result = damage - resistentAmount;
        return result;
    }

    private void ControlBurn()
    {
        int value = Random.Range(0, 100);
        if (value < burnChance)
        {
            BurnImpact impact = impacts.TryGetImpact<BurnImpact>(typeof(BurnImpact));

            if (impact != null)
            {
                impact.DoubleUpDamage();
            }

            else
            {
                BurnImpact burnImpact = new BurnImpact(burnCount, impacts, impactSprite);
                burnImpact.Init(health, burnDamage, resistence);
                impacts.AddPassiveImpact(burnImpact, impactDescription, ImpactType.EnemyDebuff);
            }
        }
    }
}
