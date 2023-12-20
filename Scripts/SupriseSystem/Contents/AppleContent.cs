using UnityEngine;

[CreateAssetMenu(fileName = "AppleContent", menuName = "SupriseContents/Apple")]

public class AppleContent : SupriseCardContent
{
    [SerializeField] private int foodAmount;
    public override void Action()
    {
        PlayerSkillManager player = CombatManager.Instance.GetPlayer();
        PlayerStuffManager stuff = player.GetComponent<PlayerStuffManager>();
        stuff.IncreaseFood(foodAmount);
    }
}
