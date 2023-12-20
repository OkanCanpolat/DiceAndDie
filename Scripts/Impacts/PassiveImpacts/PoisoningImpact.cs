using UnityEngine;

public class PoisoningImpact : Impact
{
    private GameObject poisoningEffect;
    private Health health;
    private Resistences resistences;
    private float poisionDamage;
    private float currentDamage;
    private int poisionChance;
    private int doubleChance;
    private int startingTurnCount;
    private int posioningMaxTurnCount;
    private DamageType poisonDamageType;
    private Vector3 explosionInstantiatePosition;
    private float poisonEffectDestoyTime = 6f;
    private DamageAmountText poisonDamageText;

    public int TurnCount { get => turnCount; set => turnCount = value; }
    public PoisoningImpact(int turnCount, ImpactManager manager, Sprite impactSprite) : base(turnCount, manager, impactSprite)
    {
    }
    public void Init(Health health, Resistences resistences, GameObject poisoningEffect, Vector3 explosionInstantiatePosition)
    {
        startingTurnCount = turnCount;
        this.poisoningEffect = poisoningEffect;
        this.health = health;
        this.resistences = resistences;
        this.explosionInstantiatePosition = explosionInstantiatePosition;
    }
    public void InitPoisionValues(int poisionChance, int posioningMaxTurnCount, int doubleChance, float poisionDamage)
    {
        this.poisionChance = poisionChance;
        this.posioningMaxTurnCount = posioningMaxTurnCount;
        this.doubleChance = doubleChance;
        this.poisionDamage = poisionDamage;
    }
    public override void OnTurnCome()
    {
        turnCount--;

        if(posioningMaxTurnCount > 0)
        {
            ControlPoision();
            posioningMaxTurnCount--;
        }

        if (currentDamage > 0)
        {
            ApplyDamage();
        }

        if (turnCount == 0)
        {
            impactManager.RemovePassiveImpact(this);
            return;
        }

        turnCountText.TurnText.text = turnCount.ToString();
    }
    public override void SetUI(GameObject icon)
    {
        base.SetUI(icon);
        poisonDamageText = icon.GetComponent<DamageAmountText>();
        poisonDamageText.DamageText.text = currentDamage.ToString();
    }
    public override void RemoveListeners()
    {
        throw new System.NotImplementedException();
    }
    private void ControlPoision()
    {
        int value = Random.Range(0, 100);

        if(value < poisionChance)
        {
            GameObject effect = Object.Instantiate(poisoningEffect, explosionInstantiatePosition, Quaternion.identity);
            Object.Destroy(effect, poisonEffectDestoyTime);
            turnCount = startingTurnCount;
            ControlPoisonStack();
            poisonDamageText.DamageText.text = currentDamage.ToString();
        }
    }

    private void ControlPoisonStack()
    {
        if(currentDamage == 0)
        {
            currentDamage = poisionDamage;
            poisonDamageType = new PoisonDamage(resistences, currentDamage);
        }

        else
        {
            ControlDoubleDamage();
        }
    }
    private void ControlDoubleDamage()
    {
        int randomChance = Random.Range(0, 100);

        if (randomChance < doubleChance)
        {
            currentDamage *= 2;
        }

        else
        {
            currentDamage += poisionDamage;
        }

        poisonDamageType = new PoisonDamage(resistences, currentDamage);

    }
    private void ApplyDamage()
    {
        health.TakeDamage(poisonDamageType);
    }
}
