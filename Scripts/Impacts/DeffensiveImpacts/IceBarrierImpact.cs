using UnityEngine;

public class IceBarrierImpact : DeffensiveImpact
{
    private GameObject effect;

    public IceBarrierImpact(int turnCount, ImpactManager impactManager, Sprite impactSprite) : base(turnCount, impactManager, impactSprite)
    {

    }

    public void Init(GameObject effect)
    {
        this.effect = effect;
    }
    public override void OnDeffense(DamageType type)
    {
        if(type is BasicAttack)
        {
            bool isBlocked = type.CalculateDamage() == 0;

            if (!isBlocked)
            {
                type.SetDamage(0);
                impactManager.RemoveDeffensiveImpact(this);
                Object.Destroy(effect);
            }
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
