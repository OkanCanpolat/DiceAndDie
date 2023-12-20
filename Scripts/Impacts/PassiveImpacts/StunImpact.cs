using UnityEngine;

public class StunImpact : Impact
{
    private IStunable character;
    public StunImpact(int turnCount, ImpactManager impactManager, Sprite impactSprite) : base(turnCount, impactManager, impactSprite)
    {

    }

    public void Init(IStunable character)
    {
        this.character = character;
    }
    public override void OnTurnCome()
    {
        turnCount--;

        if (turnCount == 0)
        {
            character.IsStunned = false;
            impactManager.RemoveEndTurnPassiveImpact(this);
            return;
        }
    }

    public override void RemoveListeners()
    {
        throw new System.NotImplementedException();
    }
}
