using UnityEngine;

[CreateAssetMenu(fileName = "SimpleDarkMagic", menuName = "Skills/Common/Simple Dark Magic")]

public class SimpleDarkMagic : Skill
{
    [SerializeField] private GameObject Prefab;
    [SerializeField] private float trueDamage;
    [SerializeField] private float selfDamage;

    private DamageType type;
    private PlayerSkillManager player;
    private Enemy target;
    public override void Use()
    {
        player = CombatManager.Instance.GetPlayer();
        Health playerHealth = player.GetComponent<Health>();
        Resistences playerResistences = player.GetComponent<Resistences>();
        type = new TrueDamage(playerResistences, selfDamage);
        playerHealth.TakeDamage(type);
        target = CombatManager.Instance.GetEnemy();
        RFX1_AnimatorEvents events = player.GetComponent<RFX1_AnimatorEvents>();
        events.Effect1.Prefab = Prefab;
    }

    public void OnCollision()
    {
        Resistences enemyResistences = target.GetComponent<Resistences>();
        type = new TrueDamage(enemyResistences, trueDamage);
        Health health = target.GetComponent<Health>();
        health.TakeDamage(type);
    }
}
