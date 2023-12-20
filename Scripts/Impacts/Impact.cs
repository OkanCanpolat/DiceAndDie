using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Impact 
{
    public Sprite impactSprite;
    protected int turnCount;
    protected ImpactManager impactManager;
    protected TurnCountText turnCountText;
    public Impact(int turnCount, ImpactManager impactManager, Sprite impactSprite)
    {
        this.turnCount = turnCount;
        this.impactManager = impactManager;
        this.impactSprite = impactSprite;
    }
    public abstract void OnTurnCome();
    public abstract void RemoveListeners();
    public virtual void SetUI(GameObject icon)
    {
        turnCountText = icon.GetComponent<TurnCountText>();
        turnCountText.TurnText.text = turnCount.ToString();
    }
    public virtual void OnDelete()
    {

    }
}
