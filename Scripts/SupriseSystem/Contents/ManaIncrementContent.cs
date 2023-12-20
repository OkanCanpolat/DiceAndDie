using UnityEngine;

[CreateAssetMenu(fileName = "ManaIncrementContent", menuName = "SupriseContents/Mana Increment")]

public class ManaIncrementContent : SupriseCardContent
{
    [SerializeField] private int manaAmount;
    public override void Action()
    {
        PlayerSkillManager player = CombatManager.Instance.GetPlayer();
        player.IncreaseMana(manaAmount);
    }
}
