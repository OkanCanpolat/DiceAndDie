using UnityEngine;

public class FogImpact : Impact
{
    private Resistences resistence;
    private DamageAmountText amountText;
    private float dodgeIncrement;

    public FogImpact(int turnCount, ImpactManager impactManager, Sprite impactSprite) : base(turnCount, impactManager, impactSprite)
    {
    }
    public void Init(Resistences resistence, float dodgeIncrement)
    {
        this.resistence = resistence;
        this.dodgeIncrement = dodgeIncrement;
        resistence.SetDodgeChange(dodgeIncrement);
    }
    public override void SetUI(GameObject icon)
    {
        base.SetUI(icon);
        amountText = icon.GetComponent<DamageAmountText>();
        amountText.DamageText.text = dodgeIncrement.ToString();
    }
    public override void OnTurnCome()
    {
        turnCount--;
        if (turnCount == 0)
        {
            resistence.SetDodgeChange(-dodgeIncrement);
            impactManager.RemovePassiveImpact(this);
        }

        turnCountText.TurnText.text = turnCount.ToString();
    }

    public override void OnDelete()
    {
        resistence.SetDodgeChange(-dodgeIncrement);
    }
    public override void RemoveListeners()
    {
        throw new System.NotImplementedException();
    }
}
