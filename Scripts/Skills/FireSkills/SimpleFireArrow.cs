
using UnityEngine;

[CreateAssetMenu(fileName = "SimpleFireArrow", menuName = "Skills/Common/Simple Fire Arrow")]

public class SimpleFireArrow : Skill
{
    [SerializeField] private GameObject shotPrefab;
    [SerializeField] private GameObject buffPrefab;
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private float fireDamage;
    [SerializeField] private float burnDamage;
    [SerializeField] private int burnChance;
    [SerializeField] private int burnCount;
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
        damageType = new FireDamage(targetResistence, fireDamage);
        health.TakeDamage(damageType);
        ControlImpact(health, targetResistence);
    }

    private void ControlImpact(Health health, Resistences targetResistence)
    {
        int value = Random.Range(0, 100);

        if (value < burnChance)
        {
            ImpactManager impacts = target.GetComponent<ImpactManager>();
            BurnImpact impact = impacts.TryGetImpact<BurnImpact>(typeof(BurnImpact));

            if (impact != null)
            {
                impact.DoubleUpDamage();
            }

            else
            {
                BurnImpact burnImpact = new BurnImpact(burnCount, impacts, impactSprite);
                burnImpact.Init(health, burnDamage, targetResistence);
                impacts.AddPassiveImpact(burnImpact, impactDescription, ImpactType.EnemyDebuff);
            }
        }
    }
}
