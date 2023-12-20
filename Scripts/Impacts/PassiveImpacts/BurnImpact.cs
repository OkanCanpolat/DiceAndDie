using UnityEngine;

public class BurnImpact : Impact
{
    private Resistences resistence;
    private Health health;
    private float damage;
    private DamageType type;
    private DamageAmountText amountText;
    private int startingTurnCount;

    public BurnImpact(int turnCount, ImpactManager impactManager, Sprite impactSprite) : base(turnCount, impactManager, impactSprite)
    {
        startingTurnCount = turnCount;
    }
    public void Init(Health health, float damage, Resistences resistence)
    {
        this.health = health;
        this.damage = damage;
        this.resistence = resistence;
        type = new FireDamage(this.resistence, this.damage);
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

    public void DoubleUpDamage()
    {
        float damage = type.GetDamage();
        damage *= 2;
        type.SetDamage(damage);
        amountText.DamageText.text = damage.ToString();
    }

    public void SetTurnCountToMax()
    {
        turnCount = startingTurnCount;
        turnCountText.TurnText.text = turnCount.ToString();
    }
}
