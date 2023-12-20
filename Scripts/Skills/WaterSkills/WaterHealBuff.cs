using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "WaterHealBuff", menuName = "Skills/Epic/Water Heal Buff")]
public class WaterHealBuff : Skill
{
    [SerializeField] private GameObject effectPrefab;
    [SerializeField] private float selfHeal;
    [SerializeField] private int turnCount;
    [SerializeField] private float healPerTurn;
    [SerializeField] private GameObject turnBuff;
    [SerializeField] private BodyParts buffPosition;
    [SerializeField] private Sprite impactSprite;
    [SerializeField] private GameObject impactDescription;
    private PlayerSkillManager player;
    private PlayerHealth playerHealth;
    private int healDelay = 3000;
    public override void Use()
    {
        player = CombatManager.Instance.GetPlayer();
        RFX1_AnimatorEvents events = player.GetComponent<RFX1_AnimatorEvents>();
        events.Effect2.Prefab = effectPrefab;
        HealSelf();
    }

    private async Task HealSelf()
    {
        await Task.Delay(healDelay);
        playerHealth = player.GetComponent<PlayerHealth>();
        playerHealth.RestoreHealth(selfHeal);
        Transform buffPos = BodyPartDatabase.Instance.GetPartTransform(buffPosition);
        GameObject buff = Instantiate(turnBuff, buffPos.position, Quaternion.identity);
        ControlImpact(buff);
    }

    private void ControlImpact(GameObject buff)
    {
        ImpactManager impacts = player.GetComponent<ImpactManager>();
        WaterHealImpact healImpact = new WaterHealImpact(turnCount, impacts, impactSprite);
        healImpact.Init(playerHealth, healPerTurn, buff);
        impacts.AddPassiveImpact(healImpact, impactDescription, ImpactType.PlayerBuff);
    }
}
