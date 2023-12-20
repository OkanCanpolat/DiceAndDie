using UnityEngine;

[CreateAssetMenu(fileName = "BurnAttack", menuName = "Skills/Rare/Burn Attack")]
public class BurnAttack : Skill
{
    [SerializeField] private GameObject Prefab;
    private PlayerSkillManager player;
    private Enemy target;
    public override void Use()
    {
        player = CombatManager.Instance.GetPlayer();
        target = CombatManager.Instance.GetEnemy();
        RFX1_AnimatorEvents events = player.GetComponent<RFX1_AnimatorEvents>();
        events.Effect1.Prefab = Prefab;
    }
    public void OnCollision()
    {
        ImpactManager impacts = target.GetComponent<ImpactManager>();
        BurnImpact impact = impacts.TryGetImpact<BurnImpact>(typeof(BurnImpact));

        if(impact != null)
        {
            impact.SetTurnCountToMax();
        }
    }
}
