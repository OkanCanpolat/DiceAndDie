using UnityEngine;

[CreateAssetMenu(fileName = "LittleMeteor", menuName = "Skills/Rare/Little Meteor")]
public class LittleMeteor : Skill
{
    [SerializeField] private GameObject Prefab;
    [SerializeField] private float fireDamage;
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
        Resistences targetResistence = target.GetComponent<Resistences>();
        damageType = new FireDamage(targetResistence, fireDamage);
        health.TakeDamage(damageType);
    }
}
