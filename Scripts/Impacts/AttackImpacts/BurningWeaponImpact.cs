using UnityEngine;

public class BurningWeaponImpact : AttackImpact
{
    private float fireDamage;
    private GameObject effect;
    private PlayerAttack attack;
    private DamageAmountText amountText;

    public BurningWeaponImpact(int turnCount, ImpactManager impactManager, Sprite impactSprite) : base(turnCount, impactManager, impactSprite)
    {
        
    }

    public void Init(GameObject effect, PlayerAttack attack, float fireDamage)
    {
        this.effect = effect;
        this.attack = attack;
        this.fireDamage = fireDamage;
    }
    
    public override void SetUI(GameObject icon)
    {
        base.SetUI(icon);
        amountText = icon.GetComponent<DamageAmountText>();
        amountText.DamageText.text = fireDamage.ToString();
    }
    public override void OnAttack()
    {
        throw new System.NotImplementedException();
    }
    
    public override void OnTurnCome()
    {
        turnCount--;

        if(turnCount == 0)
        {
            Object.Destroy(effect);
            attack.ResetDamageType();
            impactManager.RemovePassiveImpact(this);
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
        Object.Destroy(effect);
        attack.ResetDamageType();
    }
}
