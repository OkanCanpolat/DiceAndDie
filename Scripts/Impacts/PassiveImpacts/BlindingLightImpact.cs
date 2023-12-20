
using UnityEngine;

public class BlindingLightImpact : Impact
{
    private Resistences resistence;
    private EnemyHealth health;
    private float damagePercentage;
    public BlindingLightImpact(int turnCount, ImpactManager impactManager, Sprite impactSprite) : base(turnCount, impactManager, impactSprite)
    {

    }
    public void Init(EnemyHealth health, Resistences resistence, float damagePercentage)
    {
        this.health = health;
        this.resistence = resistence;
        this.damagePercentage = damagePercentage;
    }
    public override void OnTurnCome()
    {
        turnCount--;

        if (turnCount == 0)
        {
            float percentage = damagePercentage / 100; 
            float damage = health.GetMaxHealth() * percentage;
            DamageType damageType = new IceDamage(resistence, damage);    
            health.TakeDamage(damageType);
            impactManager.RemovePassiveImpact(this);
        }

        turnCountText.TurnText.text = turnCount.ToString();
    }
    public override void RemoveListeners()
    {
        throw new System.NotImplementedException();
    }
}
