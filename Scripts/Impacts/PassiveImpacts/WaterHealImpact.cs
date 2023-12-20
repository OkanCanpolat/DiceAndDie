using UnityEngine;

public class WaterHealImpact : Impact
{
    private GameObject healthEffect;
    private PlayerHealth playerHealth;
    private float healthAmount;
    private DamageAmountText healAmountText;

    public WaterHealImpact(int turnCount, ImpactManager manager, Sprite impactSprite) : base(turnCount, manager, impactSprite)
    {

    }

    public void Init(PlayerHealth playerHealth, float healthAmount, GameObject healthEffect)
    {
        this.playerHealth = playerHealth;
        this.healthAmount = healthAmount;
        this.healthEffect = healthEffect;
    }
    
    public override void OnTurnCome()
    {
        playerHealth.RestoreHealth(healthAmount);

        turnCount--;

        if (turnCount == 0)
        {
            impactManager.RemovePassiveImpact(this);
            Object.Destroy(healthEffect);
            return;
        }

        turnCountText.TurnText.text = turnCount.ToString();
    }

    public override void RemoveListeners()
    {
        throw new System.NotImplementedException();
    }
}
