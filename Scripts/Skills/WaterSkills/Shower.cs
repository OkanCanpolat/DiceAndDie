using UnityEngine;

[CreateAssetMenu(fileName = "Shower", menuName = "Skills/Rare/Shower")]
public class Shower : Skill
{
    [SerializeField] private GameObject Prefab;
    [SerializeField] private float waterDamage;
    [SerializeField] private float selfHeal;
    [SerializeField] private int wetTurnCount;
    [SerializeField] private Sprite impactSprite;
    [SerializeField] private GameObject impactDescription;

    private DamageType damageType;
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
        Health health = target.GetComponent<Health>();
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        Resistences targetResistence = target.GetComponent<Resistences>();
        damageType = new WaterDamage(targetResistence, waterDamage);
        health.TakeDamage(damageType);
        playerHealth.RestoreHealth(selfHeal);
        ControlImpact();
    }

    private void ControlImpact()
    {
        ImpactManager impacts = target.GetComponent<ImpactManager>();

        if (!impacts.HasPassiveImpact(typeof(WetImpact)))
        {
            WetImpact wetImpact = new WetImpact(wetTurnCount, impacts, impactSprite);
            impacts.AddPassiveImpact(wetImpact, impactDescription, ImpactType.EnemyDebuff);
        }
    }
}
