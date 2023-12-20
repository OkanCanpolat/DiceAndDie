using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "Water Protection", menuName = "Skills/Epic/Water Protection")]

public class WaterProtection : Skill
{
    [SerializeField] private GameObject effectPrefab;
    [SerializeField] private GameObject healEffectPrefab;
    [SerializeField] private int turnCount;
    [SerializeField] private float effectDestroyTime;
    [SerializeField] private float regenerationPercent;
    [SerializeField] private int barrierCreationDelayMS;
    [SerializeField] private Sprite impactSprite;
    [SerializeField] private GameObject impactDescription;

    private PlayerSkillManager player;

    public override void Use()
    {
        player = CombatManager.Instance.GetPlayer();
        GameObject skill = Instantiate(effectPrefab, player.transform.position, Quaternion.identity);
        GenerateBarrier(skill);
    }

    private async Task GenerateBarrier(GameObject effect)
    {
        await Task.Delay(barrierCreationDelayMS);
        ImpactManager impacts = player.GetComponent<ImpactManager>();
        WaterProtectionImpact impact = new WaterProtectionImpact(turnCount, impacts, impactSprite);
        impact.Init(effect, player, healEffectPrefab);
        impacts.AddDeffensiveImpact(impact, impactDescription, ImpactType.PlayerBuff); ;
    }
}
