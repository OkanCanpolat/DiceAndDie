using UnityEngine;

public class EssenceHarvestImpact : Impact
{
    private PlayerSkillManager player;
    private int manaStealAmount;
    public EssenceHarvestImpact(int turnCount, ImpactManager impactManager, Sprite impactSprite) : base(turnCount, impactManager, impactSprite)
    {

    }

    public void Init(PlayerSkillManager player, int manaStealAmount)
    {
        this.player = player;
        this.manaStealAmount = manaStealAmount;
    }
    public override void OnTurnCome()
    {
        turnCount--;

        if (turnCount == 0)
        {
            impactManager.RemovePassiveImpact(this);
            int currentMana = player.GetLastTurnMana();
            int newMana = currentMana + manaStealAmount;
            player.SetLastTurnMana(newMana);
            return;
        }

        turnCountText.TurnText.text = turnCount.ToString();
    }

    public override void SetUI(GameObject icon)
    {
        base.SetUI(icon);
        DamageAmountText text = icon.GetComponent<DamageAmountText>();
        text.DamageText.text = manaStealAmount.ToString();
    }

    public override void RemoveListeners()
    {
        throw new System.NotImplementedException();
    }
}
