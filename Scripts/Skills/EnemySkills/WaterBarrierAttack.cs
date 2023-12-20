using UnityEngine;
using System.Threading.Tasks;

[CreateAssetMenu(fileName = "WaterBarrierAttack", menuName = "Skills/Enemy/Water Barrier Attack")]
public class WaterBarrierAttack : Skill
{
    [SerializeField] private GameObject skillEffect;
    [SerializeField] private float effectLifetime;
    [SerializeField] private int damageDelayMS;
    [SerializeField] private float waterDamage;
    [SerializeField] private int turnCount;
    [SerializeField] private Sprite impactSprite;
    [SerializeField] private GameObject impactDescription;
    private PlayerHealth playerHealth;
    private Resistences playerResistence;
    private DamageType damageType;
    public override void Use()
    {
        PlayerSkillManager player = CombatManager.Instance.GetPlayer();
        playerHealth = player.GetComponent<PlayerHealth>();
        playerResistence = player.GetComponent<Resistences>();
        GameObject effect = Instantiate(skillEffect, player.transform.position, Quaternion.identity);
        Destroy(effect, effectLifetime);
        Damage();
    }

    private async Task Damage()
    {
        await Task.Delay(damageDelayMS);
        damageType = new WaterDamage(playerResistence, waterDamage);
        playerHealth.TakeDamage(damageType);
        ControlImpact();
    }

    private void ControlImpact()
    {
        ImpactManager impacts = playerHealth.GetComponent<ImpactManager>();

        if (!impacts.HasPassiveImpact(typeof(WetImpact)))
        {
            WetImpact wetImpact = new WetImpact(turnCount, impacts, impactSprite);
            impacts.AddPassiveImpact(wetImpact, impactDescription, ImpactType.PlayerDebuff);
        }
    }
}
