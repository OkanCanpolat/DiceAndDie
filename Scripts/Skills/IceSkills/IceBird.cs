using UnityEngine;

[CreateAssetMenu(fileName = "IceBird", menuName = "Skills/Rare/Ice Bird")]
public class IceBird : Skill
{
    [SerializeField] private GameObject shotPrefab;
    [SerializeField] private GameObject buffPrefab;
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private float IceDamage;
    [SerializeField] private int stunChange;
    [SerializeField] private float freezeChange;
    [SerializeField] private int freezeTurnCount;
    [SerializeField] private Sprite stunImpactSprite;
    [SerializeField] private GameObject stunImpactDescription;
    [SerializeField] private Sprite freezeImpactSprite;
    [SerializeField] private GameObject freezeImpactDescription;
    private const int stunTurnCount = 1;

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
        ImpactManager impacts = target.GetComponent<ImpactManager>();
        float damage = ControlFreezeDamage(impacts);
        damageType = new IceDamage(targetResistence, damage);
        health.TakeDamage(damageType);
        ControlStun(impacts);
    }

    private void ControlStun(ImpactManager impacts)
    {
        IStunable stunTarget = target as IStunable;
        if (stunTarget == null) return;

        int value = Random.Range(0, 100);

        if (value < stunChange)
        {
            if (!impacts.HasPassiveImpact(typeof(StunImpact)))
            {
                StunImpact stunImpact = new StunImpact(stunTurnCount, impacts, stunImpactSprite);
                impacts.AddEndTurnPassiveImpact(stunImpact, stunImpactDescription, ImpactType.EnemyDebuff);
                stunTarget.IsStunned = true;
                stunImpact.Init(stunTarget);
            }
        }
    }

    private float ControlFreezeDamage(ImpactManager impacts)
    {
        int damageMultiplier = 1;

        FreezeImpact freeze = impacts.TryGetImpact<FreezeImpact>(typeof(FreezeImpact));

        if (freeze != null)
        {
            damageMultiplier = 2;
            freeze.DestroyImpact();
        }

        else
        {
            int value = Random.Range(0, 100);

            if (value < freezeChange)
            {
                FreezeImpact freezeImpact = new FreezeImpact(freezeTurnCount, impacts, freezeImpactSprite);
                impacts.AddPassiveImpact(freezeImpact, freezeImpactDescription, ImpactType.EnemyDebuff);
            }
        }

        return IceDamage * damageMultiplier;
    }
}
