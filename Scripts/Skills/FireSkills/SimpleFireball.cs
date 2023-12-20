using UnityEngine;

[CreateAssetMenu(fileName = "SimpleFireball", menuName = "Skills/Common/Simple Fireball")]

public class SimpleFireball : Skill
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float fireDamage;
    [SerializeField] private float burnChance;
    [SerializeField] private float burnDamage;
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
        RFX1_AnimatorEvents events = player.GetComponent<RFX1_AnimatorEvents>();
        events.Effect1.Prefab = prefab;
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

        if(value < burnChance)
        {
            ImpactManager impacts = target.GetComponent<ImpactManager>();
            BurnImpact impact = impacts.TryGetImpact<BurnImpact>(typeof (BurnImpact));
            
            if(impact != null)
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
