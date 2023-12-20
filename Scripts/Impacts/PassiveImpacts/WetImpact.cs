using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WetImpact : Impact
{
    public WetImpact(int turnCount, ImpactManager manager, Sprite sprite) : base(turnCount, manager, sprite)
    {
        
    }
    public override void OnTurnCome()
    {
        turnCount--;

        if(turnCount == 0)
        {
            impactManager.RemovePassiveImpact(this);
            return;
        }
        turnCountText.TurnText.text = turnCount.ToString();
    }

    public override void RemoveListeners()
    {
        throw new System.NotImplementedException();
    }
}
