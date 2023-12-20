using UnityEngine;
using System.Threading.Tasks;

[CreateAssetMenu(fileName = "SummonStorm", menuName = "Skills/Enemy/Summon Storm")]
public class SummonStorm : Skill
{
    [SerializeField] private GameObject skillEffect;
    [SerializeField] private float effectLifetime;
    [SerializeField] private int damageDelayMS;
    [SerializeField] private float electricDamage;
    private PlayerHealth playerHealth;
    private Resistences playerResistence;
    private DamageType damageType;
    private ImpactManager impacts;
    public override void Use()
    {
        PlayerSkillManager player = CombatManager.Instance.GetPlayer();
        playerHealth = player.GetComponent<PlayerHealth>();
        playerResistence = player.GetComponent<Resistences>();
        impacts = player.GetComponent<ImpactManager>();
        GameObject effect = Instantiate(skillEffect, player.transform.position, Quaternion.identity);
        Destroy(effect, effectLifetime);
        Damage();
    }

    private async Task Damage()
    {
        await Task.Delay(damageDelayMS);
        WetImpact impact = null;
        int damageMultiplier = 1;
        bool targetIsWet = ControlWetImpact(out impact);

        if (targetIsWet)
        {
            damageMultiplier = 2;
        }

        damageType = new ElectricDamage(playerResistence, electricDamage * damageMultiplier);
        playerHealth.TakeDamage(damageType);

        if (targetIsWet)
        {
            impacts.RemovePassiveImpact(impact);
        }
    }

    private bool ControlWetImpact(out WetImpact impact)
    {
        impact = impacts.TryGetImpact<WetImpact>(typeof(WetImpact));
        if (impact != null) return true;
        return false;
    }
}
