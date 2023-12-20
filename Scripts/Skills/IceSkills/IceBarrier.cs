using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "IceBarrier", menuName = "Skills/Rare/Ice Barrier")]

public class IceBarrier : Skill
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private BodyParts effectPosition;
    [SerializeField] private int turnCount;
    [SerializeField] private Sprite impactSprite;
    [SerializeField] private GameObject impactDescription;
    private PlayerSkillManager player;
    private int buffDelay = 3000;
    public override void Use()
    {
        player = CombatManager.Instance.GetPlayer();
        Transform effectPos = BodyPartDatabase.Instance.GetPartTransform(effectPosition);
        GameObject effect = Instantiate(prefab, effectPos.position, Quaternion.identity);
        GenerateBuff(effect);
    }

    private async Task GenerateBuff(GameObject effect)
    {
        await Task.Delay(buffDelay);
        ImpactManager impacts = player.GetComponent<ImpactManager>();
        IceBarrierImpact impact = new IceBarrierImpact(turnCount, impacts, impactSprite);
        impact.Init(effect);
        impacts.AddDeffensiveImpact(impact, impactDescription, ImpactType.PlayerBuff);
    }
}
