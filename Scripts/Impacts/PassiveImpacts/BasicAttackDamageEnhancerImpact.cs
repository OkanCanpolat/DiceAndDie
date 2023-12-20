using UnityEngine;

public class BasicAttackDamageEnhancerImpact : Impact
{
    private float damageBonus;
    private Enemy enemy;
    public BasicAttackDamageEnhancerImpact(int turnCount, ImpactManager impactManager, Sprite impactSprite) : base(turnCount, impactManager, impactSprite)
    {

    }
    public void Init(float damageBonus, Enemy enemy)
    {
        this.damageBonus = damageBonus;
        this.enemy = enemy;
    }
    public override void OnTurnCome()
    {
        turnCount--;

        if (turnCount == 0)
        {
            impactManager.RemovePassiveImpact(this);
            enemy.ResetBasicAttackDamage();
            return;
        }

        turnCountText.TurnText.text = turnCount.ToString();
    }

    public override void SetUI(GameObject icon)
    {
        base.SetUI(icon);
        DamageAmountText text = icon.GetComponent<DamageAmountText>();
        text.DamageText.text = damageBonus.ToString();
    }
    public override void RemoveListeners()
    {
        throw new System.NotImplementedException();
    }
}
