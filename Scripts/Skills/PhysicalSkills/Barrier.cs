using UnityEngine;
using System.Threading.Tasks;

[CreateAssetMenu(fileName = "Barrier", menuName = "Skills/Rare/Barrier")]
public class Barrier : Skill
{
    [SerializeField] private GameObject effectPrefab;
    [SerializeField] private float effectDestroyTime;
    [SerializeField] private BodyParts effectPosition;
    [SerializeField] private int turnCount;
    [SerializeField] private int barrierAmount;
    [SerializeField] private Sprite impactSprite;
    [SerializeField] private GameObject impactDescription;
    private PlayerSkillManager player;
    private int buffDelay = 3000;
    public override void Use()
    {
        player = CombatManager.Instance.GetPlayer();
        Transform effectPos = BodyPartDatabase.Instance.GetPartTransform(effectPosition);
        GameObject effect = Instantiate(effectPrefab, effectPos.position, Quaternion.identity);
        Destroy(effect, effectDestroyTime);
        GenerateBuff();
    }
    private async Task GenerateBuff()
    {
        await Task.Delay(buffDelay);
        ImpactManager impacts = player.GetComponent<ImpactManager>();
        BarrierImpact impact = new BarrierImpact(turnCount, impacts, impactSprite);
        impact.Init(barrierAmount);
        impact.SetUI(impactDescription);
        impacts.AddDeffensiveImpact(impact, impactDescription, ImpactType.PlayerBuff);
    }
}
