using UnityEngine;

public class DarkFormImpact : DeffensiveImpact
{
    private CharacterViewChanger changer;
    private float damagePercentage;
    private DamageAmountText amountText;
    public DarkFormImpact(int turnCount, ImpactManager impactManager, Sprite impactSprite) : base(turnCount, impactManager, impactSprite)
    {

    }
    public void Init(float damagePercentage, CharacterViewChanger changer)
    {
        this.damagePercentage = damagePercentage;
        this.changer = changer;
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

        if(turnCount == 0)
        {
            impactManager.RemoveDeffensiveImpact(this);
            changer.ChangeViewToOriginal();
            changer.ActivateClothesAndHair();
        }

        turnCountText.TurnText.text = turnCount.ToString();
    }

    public override void RemoveListeners()
    {
        throw new System.NotImplementedException();
    }
}
