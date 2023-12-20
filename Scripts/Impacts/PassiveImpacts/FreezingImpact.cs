using UnityEngine;

public class FreezingImpact : Impact
{
    private GameObject explosionEffect;
    private GameObject bigExplosionEffect;
    private Health health;
    private Resistences resistences;
    private int freezeAmount;
    private int freezeChance;
    private float explosionDamage;
    private float bigExplosionDamage;
    private int stunChance;
    private DamageType explosionDamageType;
    private DamageType bigExplosionDamageType;
    private int explosionPerXTurn;
    private DamageAmountText freezeAmountText;
    private Vector3 explosionInstantiatePýsition;
    private int stunTurnCount;
    private Sprite stunImpactSprite;
    private GameObject stunImpactDescription;
    private ImpactType impactType;
    private float explosionEffectDestoyTime = 6f;
    public FreezingImpact(int turnCount, ImpactManager manager, Sprite impactSprite) : base(turnCount, manager, impactSprite)
    {
    }

    public void InitExplosionDamages(float explosionDamage, float bigExplosionDamage)
    {
        this.explosionDamage = explosionDamage;
        this.bigExplosionDamage = bigExplosionDamage;
    }
    public void Init(Health health, Resistences resistences, GameObject explosionEffect, GameObject bigExplosionEffect, int freezeChance, int explosionPerXTurn, Vector3 explosionInstantiatePýsition)
    {
        this.explosionEffect = explosionEffect;
        this.bigExplosionEffect = bigExplosionEffect;
        this.freezeChance = freezeChance;
        this.health = health;
        this.resistences = resistences;
        this.explosionPerXTurn = explosionPerXTurn;
        this.explosionInstantiatePýsition = explosionInstantiatePýsition;
        explosionDamageType = new IceDamage(this.resistences, explosionDamage);
        bigExplosionDamageType = new IceDamage(this.resistences, bigExplosionDamage);
    }
    public void InitStunProperties(int stunTurnCount, Sprite stunImpactSprite, GameObject stunImpactDescription, int stunChance, ImpactType impactType)
    {
        this.stunTurnCount = stunTurnCount;
        this.stunImpactDescription = stunImpactDescription;
        this.stunImpactSprite = stunImpactSprite;
        this.stunChance = stunChance;
        this.impactType = impactType;
    }
    public override void SetUI(GameObject icon)
    {
        base.SetUI(icon);
        freezeAmountText = icon.GetComponent<DamageAmountText>();
        freezeAmountText.DamageText.text = freezeAmount.ToString();
        StackCountText freezeChanceText = icon.GetComponent<StackCountText>();
        freezeChanceText.StackText.text = freezeChance.ToString();
    }
    public override void OnTurnCome()
    {
        turnCount--;

        ControlFreeze();

        if (turnCount == 0)
        {
            impactManager.RemovePassiveImpact(this);
            return;
        }

        freezeAmountText.DamageText.text = freezeAmount.ToString();
        turnCountText.TurnText.text = turnCount.ToString();
    }

    public override void RemoveListeners()
    {
        throw new System.NotImplementedException();
    }
    private void ControlFreeze()
    {
        int number = Random.Range(0, 100);

        if (number < freezeChance)
        {
            freezeAmount++;

            if (freezeAmount < explosionPerXTurn)
            {
                health.TakeDamage(explosionDamageType);
                GameObject explosion = Object.Instantiate(explosionEffect, explosionInstantiatePýsition, Quaternion.identity);
                Object.Destroy(explosion, explosionEffectDestoyTime);
            }

            else
            {
                ControlStun();
                freezeAmount = 0;
                health.TakeDamage(bigExplosionDamageType);
                GameObject explosion = Object.Instantiate(bigExplosionEffect, explosionInstantiatePýsition, Quaternion.identity);
                Object.Destroy(explosion, explosionEffectDestoyTime);
            }
        }
    }

    private void ControlStun()
    {
        int value = Random.Range(0, 100);
        IStunable character = health.gameObject.GetComponent<Enemy>() as IStunable;

        if (character != null && value < stunChance)
        {
            character.IsStunned = true;
            StunImpact impact = new StunImpact(stunTurnCount, impactManager, stunImpactSprite);
            impactManager.AddEndTurnPassiveImpact(impact, stunImpactDescription, impactType);
        }
    }
}
