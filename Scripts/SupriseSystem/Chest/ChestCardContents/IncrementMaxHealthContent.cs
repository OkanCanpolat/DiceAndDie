using UnityEngine;

[CreateAssetMenu(fileName = "IncrementMaxHealthContent", menuName = "SupriseContents/Chest/Increase Max Health")]

public class IncrementMaxHealthContent : SupriseCardContent
{
    [SerializeField] private float maxHealthIncrement;
    public override void Action()
    {
        PlayerSkillManager player = CombatManager.Instance.GetPlayer();
        PlayerHealth health = player.GetComponent<PlayerHealth>();
        health.IncreaseMaxHealth(maxHealthIncrement);
    }
}
