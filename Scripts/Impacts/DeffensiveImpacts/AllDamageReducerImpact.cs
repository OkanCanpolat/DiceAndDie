using UnityEngine;

public class AllDamageReducerImpact : DeffensiveImpact
{
    private float reducePercent;
    public AllDamageReducerImpact(int turnCount, ImpactManager impactManager, Sprite impactSprite) : base(turnCount, impactManager, impactSprite)
    {

    }
    public void Init(float reducePercent)
    {
        this.reducePercent = reducePercent;
    }
    public override void OnDeffense(DamageType type)
    {
        if(!(type is TrueDamage))
        {
            bool isBlocked = type.CalculateDamage() == 0;

            if (!isBlocked)
            {
                float damage = type.GetDamage();
                float reducedDamage = damage * (reducePercent / 100);
                float newDamage = damage - reducedDamage;
                type.SetDamage(newDamage);
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

    public override void SetUI(GameObject icon)
    {
        base.SetUI(icon);
        DamageAmountText damageReduceText = icon.GetComponent<DamageAmountText>();
        damageReduceText.DamageText.text = reducePercent.ToString();
    }
    public override void RemoveListeners()
    {
        throw new System.NotImplementedException();
    }
}
