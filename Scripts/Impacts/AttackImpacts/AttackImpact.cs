
using UnityEngine;

public abstract class AttackImpact : Impact
{
    protected AttackImpact(int turnCount, ImpactManager impactManager, Sprite impactSprite) : base(turnCount, impactManager, impactSprite)
    {
    }

    public abstract void OnAttack();
}
