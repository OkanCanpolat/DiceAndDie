using UnityEngine;

[CreateAssetMenu(fileName = "GhostCircles", menuName = "Skills/Rare/Ghost Circles")]
public class GhostCircles : Skill
{
    [SerializeField] private GameObject Prefab;
    [SerializeField] private float minTrueDamage;
    [SerializeField] private float maxTrueDamage;
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
        float randomDamage = GetRandomDamage();
        Health health = target.GetComponent<Health>();
        Resistences enemyResistences = target.GetComponent<Resistences>();
        type = new TrueDamage(enemyResistences, GetRandomDamage());
        health.TakeDamage(type);
    }
    private float GetRandomDamage()
    {
        float randomDamage = Random.Range(minTrueDamage, maxTrueDamage);
        return randomDamage;
    }
}
