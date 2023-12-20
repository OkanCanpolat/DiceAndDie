using UnityEngine;

[CreateAssetMenu(fileName = "SimplePoisonArrow", menuName = "Skills/Common/Simple Poison Arrow")]

public class SimplePoisonArrow : Skill
{
    [SerializeField] private GameObject shotPrefab;
    [SerializeField] private GameObject buffPrefab;
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private float physicalDamage;
    [SerializeField] private float poisonDamage;
    [SerializeField] private int poisonChange;
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
        damageType = new PhysicalDamage(targetResistence, physicalDamage);
        health.TakeDamage(damageType);
        ControlImpact(health, targetResistence);
    }

    private void ControlImpact(Health health, Resistences targetResistence)
    {
        int value = Random.Range(0, 100);

        if (value < poisonChange)
        {
            ImpactManager impacts = target.GetComponent<ImpactManager>();

            PoisonImpact poisonImpact = new PoisonImpact(turnCount, impacts, impactSprite);
            poisonImpact.Init(health, poisonDamage, targetResistence);
            impacts.AddPassiveImpact(poisonImpact, impactDescription, ImpactType.EnemyDebuff);
        }
    }
}
