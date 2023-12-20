using UnityEngine;

public class SnowyImpact : Impact
{
    private GameObject snowEffect;
    public SnowyImpact(int turnCount, ImpactManager manager, Sprite impactSprite) : base(turnCount, manager, impactSprite)
    {

    }

    public void Init(GameObject snowEffect)
    {
        this.snowEffect = snowEffect;
    }
    public override void OnTurnCome()
    {
        turnCount--;

        if (turnCount == 0)
        {
            impactManager.RemovePassiveImpact(this);
            Object.Destroy(snowEffect);
            return;
        }

        turnCountText.TurnText.text = turnCount.ToString();
    }

    public override void RemoveListeners()
    {
        throw new System.NotImplementedException();
    }
    public override void OnDelete()
    {
        Object.Destroy(snowEffect);
    }
}
