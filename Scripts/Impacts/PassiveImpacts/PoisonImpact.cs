using UnityEngine;

public class PoisonImpact : Impact
{
    private Resistences resistence;
    private Health health;
    private float damage;
    private DamageType type;
    private DamageAmountText amountText;

    public PoisonImpact(int turnCount, ImpactManager impactManager, Sprite impactSprite) : base(turnCount, impactManager, impactSprite)
    {

    }
    public void Init(Health health, float damage, Resistences resistence)
    {
        this.health = health;
        this.damage = damage;
        this.resistence = resistence;
        type = new PoisonDamage(this.resistence, this.damage);
    }

    public override void SetUI(GameObject icon)
    {
        base.SetUI(icon);
        amountText = icon.GetComponent<DamageAmountText>();
        amountText.DamageText.text = damage.ToString();
    }
    public override void OnTurnCome()
    {
        health.TakeDamage(type);

        turnCount--;
        if (turnCount == 0)
        {
            impactManager.RemovePassiveImpact(this);
        }
        turnCountText.TurnText.text = turnCount.ToString();
    }
    public override void RemoveListeners()
    {
        throw new System.NotImplementedException();
    }
}
