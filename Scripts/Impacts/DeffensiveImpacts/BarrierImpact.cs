using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BarrierImpact : DeffensiveImpact
{
    private int barrierAmount;
    private DamageAmountText barrierAmountText;
    public BarrierImpact(int turnCount, ImpactManager impactManager, Sprite impactSprite) : base(turnCount, impactManager, impactSprite)
    {

    }
    public void Init(int barrierAmount)
    {
        this.barrierAmount = barrierAmount;
    }
    public override void SetUI(GameObject icon)
    {
        base.SetUI(icon);
        barrierAmountText = icon.GetComponent<DamageAmountText>();
        barrierAmountText.DamageText.text = barrierAmount.ToString();
    }
    public override void OnDeffense(DamageType type)
    {
        float realDamage = type.GetDamage();
        float finalBarrier = barrierAmount - realDamage;

        if (finalBarrier <= 0)
        {
            type.SetDamage(Mathf.Abs(finalBarrier));
            impactManager.RemoveDeffensiveImpact(this);
        }
        else
        {
            type.SetDamage(0);
            barrierAmount = (int)finalBarrier;
            barrierAmountText.DamageText.text = barrierAmount.ToString();
        }
    }

    public override void OnTurnCome()
    {
        turnCount--;

        if (turnCount == 0)
        {
            impactManager.RemoveDeffensiveImpact(this);
            return;
        }

        turnCountText.TurnText.text = turnCount.ToString();
    }

    public override void RemoveListeners()
    {
        throw new System.NotImplementedException();
    }
}
