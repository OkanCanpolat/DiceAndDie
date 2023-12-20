using System;
using Unity.Android.Types;
using UnityEngine;

[CreateAssetMenu(fileName = "ElectricBall", menuName = "Skills/Rare/Electric Ball")]

public class ElectricBall : Skill
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float electricDamage;
    [SerializeField] private int strikeChance = 20;
    [SerializeField] private int stunChance;
    [SerializeField] private Sprite stunImpactSprite;
    [SerializeField] private GameObject stunImpactDescription;

    private DamageType electricType;
    private PlayerSkillManager player;
    private Enemy target;
    private int stunTurnCount = 1;
    public override void Use()
    {
        player = CombatManager.Instance.GetPlayer();
        target = CombatManager.Instance.GetEnemy();
        RFX1_AnimatorEvents events = player.GetComponent<RFX1_AnimatorEvents>();
        events.Effect1.Prefab = prefab;
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
        int random = UnityEngine.Random.Range(0, 100);

        if (random < strikeChance)
        {
            health.TakeDamage(electricType);
        }

        if (targetIsWet)
        {
            impacts.RemovePassiveImpact(impact);
        }

        ControlStun(impacts);
    }

    private bool ControlWetImpact(out WetImpact impact, ImpactManager impacts)
    {
        impacts = target.GetComponent<ImpactManager>();
        impact = impacts.TryGetImpact<WetImpact>(typeof(WetImpact));
        if (impact != null) return true;
        return false;
    }

    private void ControlStun(ImpactManager impacts)
    {
        IStunable stunable = target as IStunable;
        if (stunable == null) return;

        int value = UnityEngine.Random.Range(0, 100);

        if (value < stunChance)
        {
            if (!impacts.HasPassiveImpact(typeof(StunImpact)))
            {
                StunImpact stunImpact = new StunImpact(stunTurnCount, impacts, stunImpactSprite);
                impacts.AddEndTurnPassiveImpact(stunImpact, stunImpactDescription, ImpactType.EnemyDebuff);
                stunable.IsStunned = true;
                stunImpact.Init(stunable);
            }
        }
    }
}
