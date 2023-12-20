using UnityEngine;

public class BurningLightImpact : Impact
{
    private GameObject turnEffect;
    private GameObject explosionEffect;
    private float currentDamage;
    private int minDamage;
    private int maxDamage;
    private int burningMinDamage;
    private int burningMaxDamage;
    private float effectDestroyTime = 2.5f;
    private Health enemyHealth;
    private DamageType damageType;
    private Resistences enemyResisteces;
    private DamageAmountText damageText;
    private Vector3 damageEffectInstantiatePosition;
    public BurningLightImpact(int turnCount, ImpactManager manager, Sprite impactSprite) : base(turnCount, manager, impactSprite)
    {

    }

    public void Init(GameObject turnEffect, GameObject explosionEffect, Health enemyHealth, Resistences enemyResisteces, Vector3 damageEffectInstantiatePosition)
    {
        this.turnEffect = turnEffect;
        this.enemyHealth = enemyHealth;
        this.enemyResisteces = enemyResisteces;
        this.explosionEffect = explosionEffect;
        this.damageEffectInstantiatePosition = damageEffectInstantiatePosition;
    }
    public void InitDamages(int minDamage, int maxDamage, int burningMinDamage, int burningMaxDamage)
    {
        this.minDamage = minDamage;
        this.maxDamage = maxDamage;
        this.burningMinDamage = burningMinDamage;
        this.burningMaxDamage = burningMaxDamage;
    }

    public override void OnTurnCome()
    {
        turnCount--;
        currentDamage += CalculateDamage();
        damageText.DamageText.text = currentDamage.ToString();

        if (turnCount == 0)
        {
            DealDamage();
            InstantiateEffect(explosionEffect);
            impactManager.RemovePassiveImpact(this);
            return;
        }

        turnCountText.TurnText.text = turnCount.ToString();
    }
    public override void SetUI(GameObject icon)
    {
        base.SetUI(icon);
        damageText = icon.GetComponent<DamageAmountText>();
        damageText.DamageText.text = currentDamage.ToString();
    }
    public override void RemoveListeners()
    {
        throw new System.NotImplementedException();
    }
    private int CalculateDamage()
    {
        BurnImpact impact = impactManager.TryGetImpact<BurnImpact>(typeof(BurnImpact));

        if (impact != null)
        {
            int damage = Random.Range(burningMinDamage, burningMaxDamage + 1);
            return damage;
        }
        else
        {
            int damage = Random.Range(minDamage, maxDamage + 1);
            return damage;
        }
    }
    private void DealDamage()
    {
        damageType = new FireDamage(enemyResisteces, currentDamage);
        enemyHealth.TakeDamage(damageType);
    }
    private void InstantiateEffect(GameObject effect)
    {
        GameObject sceneEffect = Object.Instantiate(effect, damageEffectInstantiatePosition, Quaternion.identity);
        Object.Destroy(sceneEffect, effectDestroyTime);
    }
}
