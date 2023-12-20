
using UnityEngine;

[CreateAssetMenu(fileName = "IncrementKeyContent", menuName = "SupriseContents/Chest/Increment Key")]

public class IncrementKeyContent : SupriseCardContent
{
    [SerializeField] private int keyAmount;
    public override void Action()
    {
        PlayerSkillManager player = CombatManager.Instance.GetPlayer();
        PlayerStuffManager stuff = player.GetComponent<PlayerStuffManager>();
        stuff.IncreaseKey(keyAmount);
    }
}
