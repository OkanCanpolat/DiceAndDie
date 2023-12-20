using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DeffensiveImpact : Impact
{
    protected DeffensiveImpact(int turnCount, ImpactManager impactManager, Sprite impactSprite) : base(turnCount, impactManager, impactSprite)
    {

    }
    public abstract void OnDeffense(DamageType type);
}
