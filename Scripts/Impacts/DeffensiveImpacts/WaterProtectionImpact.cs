using UnityEngine;

public class WaterProtectionImpact : DeffensiveImpact
{
    private GameObject effect;
    private float healPercentage;
    private PlayerSkillManager player;
    private PlayerHealth playerHealth;
    private GameObject healEffect;

    public WaterProtectionImpact(int turnCount, ImpactManager impactManager, Sprite impactSprite) : base(turnCount, impactManager, impactSprite)
    {

    }
    public void Init(GameObject effect, PlayerSkillManager player, GameObject healEffect)
    {
        this.effect = effect;
        this.player = player;
        playerHealth = player.GetComponent<PlayerHealth>();
        this.healEffect = healEffect;
    }
    public override void OnDeffense(DamageType type)
    {
        bool isBlocked = type.CalculateDamage() == 0;

        if (!isBlocked)
        {
            float damage = type.GetDamage();
            playerHealth.RestoreHealth(damage * healPercentage);
            Object.Instantiate(healEffect, player.transform.position, Quaternion.identity);
        }
    }

    public override void OnTurnCome()
    {
        turnCount--;

        if (turnCount == 0)
        {
            impactManager.RemoveDeffensiveImpact(this);
            Object.Destroy(effect);
            return;
        }

        turnCountText.TurnText.text = turnCount.ToString();
    }
    public override void RemoveListeners()
    {
        throw new System.NotImplementedException();
    }
}
