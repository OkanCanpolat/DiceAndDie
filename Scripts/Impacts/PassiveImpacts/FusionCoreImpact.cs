
using UnityEngine;

public class FusionCoreImpact : Impact
{
    private int damageMultiplier;
    public FusionCoreImpact(int turnCount, ImpactManager impactManager, Sprite impactSprite) : base(turnCount, impactManager, impactSprite)
    {

    }
    public override void OnTurnCome()
    {
        turnCount--;

        if (turnCount == 0)
        {
            impactManager.RemovePassiveImpact(this);
            return;
        }
        turnCountText.TurnText.text = turnCount.ToString();
    }

    public void Init(int damageMultiplier)
    {
        this.damageMultiplier = damageMultiplier;
    }

    public override void SetUI(GameObject icon)
    {
        base.SetUI(icon);
        DamageAmountText text = icon.GetComponent<DamageAmountText>();
        text.DamageText.text = damageMultiplier.ToString();
    }

    public override void RemoveListeners()
    {
        throw new System.NotImplementedException();
    }
}
