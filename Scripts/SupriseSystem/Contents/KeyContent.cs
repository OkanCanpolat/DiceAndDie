using UnityEngine;

[CreateAssetMenu(fileName = "KeyContent", menuName = "SupriseContents/Key")]

public class KeyContent : SupriseCardContent
{
    [SerializeField] private int keyAmount;
    public override void Action()
    {
        PlayerSkillManager player = CombatManager.Instance.GetPlayer();
        PlayerStuffManager health = player.GetComponent<PlayerStuffManager>();
        health.IncreaseKey(keyAmount);
    }
}
