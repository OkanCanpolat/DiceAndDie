using UnityEngine;

public class EnlightenmentImpact : Impact
{
    private PlayerHealth health;
    private float healthRestorePercent;

    public EnlightenmentImpact(int turnCount, ImpactManager impactManager, Sprite impactSprite) : base(turnCount, impactManager, impactSprite)
    {

    }
    public void Init(PlayerHealth health, float healthRestorePercent)
    {
        this.health = health;
        this.healthRestorePercent = healthRestorePercent;
    }
    public override void OnTurnCome()
    {
        turnCount--;

        if (turnCount == 0)
        {
            float percentage = healthRestorePercent / 100;
            float healAmount = health.GetMaxHealth() * percentage;
            health.RestoreHealth(healAmount);
            impactManager.RemovePassiveImpact(this);
        }

        turnCountText.TurnText.text = turnCount.ToString();
    }
    public override void RemoveListeners()
    {
        throw new System.NotImplementedException();
    }
}
