using UnityEngine;


[CreateAssetMenu(fileName = "WaterRefresh", menuName = "Skills/Common/Water Refresh")]
public class WaterRefresh : Skill
{
    [SerializeField] private GameObject Prefab;
    [SerializeField] private float healAmount;
    private PlayerSkillManager player;
    public override void Use()
    {
        player = CombatManager.Instance.GetPlayer();
        RFX1_AnimatorEvents events = player.GetComponent<RFX1_AnimatorEvents>();
        events.Effect2.Prefab = Prefab;
        PlayerHealth health = player.GetComponent<PlayerHealth>();
        health.RestoreHealth(healAmount);
    }
}
