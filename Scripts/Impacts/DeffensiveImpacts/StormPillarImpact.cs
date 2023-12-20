using UnityEngine;

public class StormPillarImpact : DeffensiveImpact
{
    private GameObject skillEffect;
    private int mirrorPercent;
    private Resistences playerResitence;
    private Health playerHealth;
    private DamageType damageType;
    public StormPillarImpact(int turnCount, ImpactManager impactManager, Sprite impactSprite) : base(turnCount, impactManager, impactSprite)
    {

    }

    public void Init(Resistences playerResitence, Health playerHealth, int mirrorPercent, GameObject skillEffect)
    {
        this.playerResitence = playerResitence;
        this.playerHealth = playerHealth;
        this.mirrorPercent = mirrorPercent;
        this.skillEffect = skillEffect;
    }
    public override void OnDeffense(DamageType type)
    {
        float damage = type.GetDamage();
        float newDamage = damage * (mirrorPercent / 100f);
        damageType = new IceDamage(playerResitence, newDamage);
        playerHealth.TakeDamage(damageType);
    }

    public override void SetUI(GameObject icon)
    {
        base.SetUI(icon);
        DamageAmountText text = icon.GetComponent<DamageAmountText>();
        text.DamageText.text = mirrorPercent.ToString();
    }
    public override void OnTurnCome()
    {
        turnCount--;

        if (turnCount == 0)
        {
            impactManager.RemovePassiveImpact(this);
            Object.Destroy(skillEffect);
            return;
        }

        turnCountText.TurnText.text = turnCount.ToString();
    }

    public override void RemoveListeners()
    {
        throw new System.NotImplementedException();
    }
}
