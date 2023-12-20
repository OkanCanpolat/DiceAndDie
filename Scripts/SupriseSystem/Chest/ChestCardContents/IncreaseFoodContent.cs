
using UnityEngine;


[CreateAssetMenu(fileName = "IncreaseFoodContent", menuName = "SupriseContents/Chest/Increase Food")]

public class IncreaseFoodContent : SupriseCardContent
{
    [SerializeField] private int foodAmount;
    public override void Action()
    {
        PlayerSkillManager player = CombatManager.Instance.GetPlayer();
        PlayerStuffManager stuff = player.GetComponent<PlayerStuffManager>();
        stuff.IncreaseFood(foodAmount);
    }
}
