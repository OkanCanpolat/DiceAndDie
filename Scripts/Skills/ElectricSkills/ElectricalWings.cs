using UnityEngine;
[CreateAssetMenu(fileName = "ElectricalWings", menuName = "Skills/Rare/Electrical Wings")]

public class ElectricalWings : Skill
{
    [SerializeField] private GameObject shotPrefab;
    [SerializeField] private GameObject buffPrefab;
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private float electricDamage;
    [SerializeField] private int strikeChange;
    private DamageType electricType;

    private PlayerSkillManager player;
    private Enemy target;
    public override void Use()
    {
        player = CombatManager.Instance.GetPlayer();
        target = CombatManager.Instance.GetEnemy();
        AE_AnimatorEvents events = player.GetComponent<AE_AnimatorEvents>();
        events.Effect1.Prefab = shotPrefab;
        events.Effect2.Prefab = weaponPrefab;
        events.Effect3.Prefab = buffPrefab;
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
