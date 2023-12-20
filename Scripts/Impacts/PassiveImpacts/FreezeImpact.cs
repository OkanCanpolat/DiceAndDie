using UnityEngine;

public class FreezeImpact : Impact
{
    public FreezeImpact(int turnCount, ImpactManager impactManager, Sprite impactSprite) : base(turnCount, impactManager, impactSprite)
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
    public override void RemoveListeners()
    {
        throw new System.NotImplementedException();
    }

    public void DestroyImpact()
    {
        impactManager.RemovePassiveImpact(this);
    }
}
