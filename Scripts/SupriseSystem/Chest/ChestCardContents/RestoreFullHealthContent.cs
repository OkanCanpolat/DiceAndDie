using UnityEngine;

[CreateAssetMenu(fileName = "RestoreFullHealthContent", menuName = "SupriseContents/Chest/Restore Full Health ")]

public class RestoreFullHealthContent : SupriseCardContent
{
    public override void Action()
    {
        PlayerSkillManager player = CombatManager.Instance.GetPlayer();
        PlayerHealth health = player.GetComponent<PlayerHealth>();
        float currentHealth = health.GetCurrentHealth();
        float maxHealth = health.GetMaxHealth();
        float increment = maxHealth - currentHealth;
        health.RestoreHealth(increment);
    }
}
