using UnityEngine;

public class UnquencableLightImpact : Impact
{
    private GameObject lightEffect;
    private GameObject healEffect;
    private int missingHealthPercentage;
    private float absorbedIntensity;
    private float changeLightSpeed = 2f;
    private PlayerHealth playerHealth;
    private float effectDestroyTime = 3f;
    private Vector3 healEffectInstantiatePosition;
    public UnquencableLightImpact(int turnCount, ImpactManager manager, Sprite impactSprite) : base(turnCount, manager, impactSprite)
    {

    }

    public void Init(GameObject lightEffect, GameObject healEffect, float absorbedIntensity,int missingHealthPercentage, PlayerHealth playerHealth, Vector3 healEffectInstantiatePosition)
    {
        this.lightEffect = lightEffect;
        this.absorbedIntensity = absorbedIntensity;
        this.playerHealth = playerHealth;
        this.healEffect = healEffect;
        this.healEffectInstantiatePosition = healEffectInstantiatePosition;
        this.missingHealthPercentage = missingHealthPercentage;
    }
    public override void OnTurnCome()
    {
        turnCount--;
        RestoreHealth();
        InstantiateEffect();

        if (turnCount == 0)
        {
            impactManager.RemovePassiveImpact(this);
            Object.Destroy(lightEffect);
            GiveBackLight();
            return;
        }

        turnCountText.TurnText.text = turnCount.ToString();
    }

    public override void RemoveListeners()
    {
        throw new System.NotImplementedException();
    }
    public override void OnDelete()
    {
        Object.Destroy(lightEffect);
        GiveBackLight();
    }
    private void GiveBackLight()
    {
        DirectionLightController.Instance.ChangeLightIntensity(changeLightSpeed, absorbedIntensity);
    }
    private void RestoreHealth()
    {
        float missingHealth = playerHealth.GetMaxHealth() - playerHealth.GetCurrentHealth();
        float resotreAmount = (missingHealthPercentage / 100f) * missingHealth;
        playerHealth.RestoreHealth(resotreAmount);
    }
    private void InstantiateEffect()
    {
        GameObject sceneEffect = Object.Instantiate(healEffect, healEffectInstantiatePosition, Quaternion.identity);
        Object.Destroy(sceneEffect, effectDestroyTime);
    }
}
