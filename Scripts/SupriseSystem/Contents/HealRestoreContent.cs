using UnityEngine;

[CreateAssetMenu (fileName = "HealContent", menuName = "SupriseContents/RestoreHeal")]
public class HealRestoreContent : SupriseCardContent
{
    [SerializeField] private float healAmount;
    public override void Action()
    {
        PlayerSkillManager player = CombatManager.Instance.GetPlayer();
        PlayerHealth health = player.GetComponent<PlayerHealth>();
        health.RestoreHealth(healAmount);
    }
}
