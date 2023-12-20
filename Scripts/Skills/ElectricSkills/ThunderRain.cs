using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "ThunderRain", menuName = "Skills/Legendary/Thunder Rain")]
public class ThunderRain : Skill
{
    [SerializeField] private GameObject Prefab;
    [SerializeField] private float damagePerThunder;
    [SerializeField] private int thunderCount = 10;
    [SerializeField] private int thunderHitChance;
    private PlayerSkillManager player;
    private Enemy target;
    private DamageType damageType;
    private int delayBetweenDamagesMS = 400;
    public override void Use()
    {
        player = CombatManager.Instance.GetPlayer();
        RFX1_AnimatorEvents events = player.GetComponent<RFX1_AnimatorEvents>();
        events.Effect2.Prefab = Prefab;
        target = CombatManager.Instance.GetEnemy();
        DealDamage();
    }
    private async Task DealDamage()
    {
        ImpactManager impacts = target.GetComponent<ImpactManager>();
        Health health = target.GetComponent<Health>();
        Resistences targetResistence = target.GetComponent<Resistences>();
        WetImpact impact = null;
        int damageMultiplier = 1;
        bool targetIsWet = ControlWetImpact(out impact, impacts);
        if (targetIsWet)
        {
            damageMultiplier = 2;
        }
        damageType = new ElectricDamage(targetResistence, damagePerThunder * damageMultiplier);
        

        for (int i = 0; i < thunderCount; i++)
        {
            await Task.Delay(delayBetweenDamagesMS);

            int value = Random.Range(0, 100);

            if(value < thunderHitChance)
            {
                health.TakeDamage(damageType);
            }

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
