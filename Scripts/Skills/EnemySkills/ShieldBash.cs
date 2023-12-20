using UnityEngine;

[CreateAssetMenu(fileName = "ShieldBash", menuName = "Skills/Enemy/Shield Bash")]

public class ShieldBash : Skill
{
    public int turnCount;
    [SerializeField] private int stunChance;
    [SerializeField] private float damage;
    [SerializeField] private Sprite impactSprite;
    [SerializeField] private GameObject impactDescription;
    private Health playerHealth;
    private Resistences resistence;
    private ImpactManager impacts;
    private PlayerSkillManager player;
    private DamageType type;

    public override void Use()
    {
        player = CombatManager.Instance.GetPlayer();
        impacts = player.GetComponent<ImpactManager>();
        resistence = player.GetComponent<Resistences>();
        playerHealth = player.GetComponent<Health>();
        type = new PhysicalDamage(resistence, damage);
        playerHealth.TakeDamage(type);
        ControlStun();
    }

    private void ControlStun()
    {
        int value = Random.Range(0, 100);

        if(value < stunChance)
        {
            StunImpact impact = new StunImpact(turnCount, impacts, impactSprite);
            impacts.AddEndTurnPassiveImpact(impact, impactDescription, ImpactType.PlayerDebuff);
            IStunable stunTarget = player.GetComponent<IStunable>();
            

            if (stunTarget != null)
            {
                stunTarget.IsStunned = true;
                impact.Init(stunTarget);
            }
        }
    }
}
