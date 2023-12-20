using UnityEngine;

public class DeffenseCurseImpact : DeffensiveImpact
{
    private float damagePercentage;
    private DamageAmountText amountText;
    public DeffenseCurseImpact(int turnCount, ImpactManager impactManager, Sprite impactSprite) : base(turnCount, impactManager, impactSprite)
    {

    }
    public void Init(float damagePercentage)
    {
        this.damagePercentage = damagePercentage;
    }
    public override void OnDeffense(DamageType type)
    {
        float currentDamage = type.GetDamage();
        float percentageResult = currentDamage * (damagePercentage / 100f);
        float resultDamage = currentDamage + percentageResult;
        type.SetDamage(resultDamage);
    }

    public override void SetUI(GameObject icon)
    {
        base.SetUI(icon);
        amountText = icon.GetComponent<DamageAmountText>();
        amountText.DamageText.text = damagePercentage.ToString();
    }
    public override void OnTurnCome()
    {
        turnCount--;

        if (turnCount == 0)
        {
            impactManager.RemoveDeffensiveImpact(this);
        }

        turnCountText.TurnText.text = turnCount.ToString();
    }

    public override void RemoveListeners()
    {
        throw new System.NotImplementedException();
    }
}
