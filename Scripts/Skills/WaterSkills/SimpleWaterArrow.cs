using UnityEngine;

[CreateAssetMenu(fileName = "SimpleWaterArrow", menuName = "Skills/Common/Simple Water Arrow")]
public class SimpleWaterArrow : Skill
{
    [SerializeField] private GameObject shotPrefab;
    [SerializeField] private GameObject buffPrefab;
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private float waterDamage;
    [SerializeField] private int turnCount;
    [SerializeField] private Sprite impactSprite;
    [SerializeField] private GameObject impactDescription;
    private DamageType damageType;
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
        Health health = target.GetComponent<Health>();
        Resistences targetResistence = target.GetComponent<Resistences>();
        damageType = new WaterDamage(targetResistence, waterDamage);
        health.TakeDamage(damageType);
        ControlImpact();
    }

    private void ControlImpact()
    {
        ImpactManager impacts = target.GetComponent<ImpactManager>();

        if (!impacts.HasPassiveImpact(typeof (WetImpact)))
        {
            WetImpact wetImpact = new WetImpact(turnCount, impacts, impactSprite);
            impacts.AddPassiveImpact(wetImpact, impactDescription, ImpactType.EnemyDebuff);
        }
    }
}
