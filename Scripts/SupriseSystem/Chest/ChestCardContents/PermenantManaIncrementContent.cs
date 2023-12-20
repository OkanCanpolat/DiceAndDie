
using UnityEngine;

[CreateAssetMenu(fileName = "PermenantManaIncrementContent", menuName = "SupriseContents/Chest/Permenant Mana Increment")]

public class PermenantManaIncrementContent : SupriseCardContent
{
    [SerializeField] private int manaAmount;
    public override void Action()
    {
        PlayerSkillManager player = CombatManager.Instance.GetPlayer();
        player.ChangeStartingManaCount(manaAmount);
    }
}
