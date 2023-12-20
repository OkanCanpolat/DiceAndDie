using UnityEngine;

[CreateAssetMenu(fileName = "ElectricMagicSimple", menuName = "Skills/Common/Simple Electric Magic")]
public class ElectricMagicSimple : Skill
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float electricDamage;
    [SerializeField] private int strikeChange = 20;
    private DamageType electricType;
    private PlayerSkillManager player;
    private Enemy target;
    public override void Use()
    {
        player = CombatManager.Instance.GetPlayer();
        target = CombatManager.Instance.GetEnemy();
        RFX1_AnimatorEvents events = player.GetComponent<RFX1_AnimatorEvents>();
        events.Effect1.Prefab = prefab;
    }

    public void OnCollision()
    {
        ImpactManager impacts = target.GetComponent<ImpactManager>();
        WetImpact impact = null;
        int damageMultiplier = 1;
        bool targetIsWet = ControlWetImpact(out impact, impacts);
        if (targetIsWet)
        {
            damageMultiplier = 2;
        }

        Health health = target.GetComponent<Health>();
        Resistences targetResistence = target.GetComponent<Resistences>();
        electricType = new ElectricDamage(targetResistence, electricDamage * damageMultiplier);
        health.TakeDamage(electricType);
        int random = Random.Range(0, 100);

        if (random < strikeChange)
        {
            health.TakeDamage(electricType);
        }

        if (targetIsWet)
        {
            impacts.RemovePassiveImpact(impact);
        }
    }

    private bool ControlWetImpact(out WetImpact impact, ImpactManager impacts)
    {
        impacts = target.GetComponent<ImpactManager>();
        impact = impacts.TryGetImpact<WetImpact>(typeof(WetImpact));
        if (impact != null) return true;
        return false;
    }
}
