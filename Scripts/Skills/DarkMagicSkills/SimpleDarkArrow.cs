
using UnityEngine;

[CreateAssetMenu(fileName = "SimpleDarkArrow", menuName = "Skills/Common/Simple Dark Arrow")]

public class SimpleDarkArrow : Skill
{
    [SerializeField] private GameObject shotPrefab;
    [SerializeField] private GameObject buffPrefab;
    [SerializeField] private GameObject weaponPrefab;
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
        AE_AnimatorEvents events = player.GetComponent<AE_AnimatorEvents>();
        events.Effect1.Prefab = shotPrefab;
        events.Effect2.Prefab = weaponPrefab;
        events.Effect3.Prefab = buffPrefab;
    }

    public void OnCollision()
    {
        Resistences enemyResistences = target.GetComponent<Resistences>();
        type = new TrueDamage(enemyResistences, trueDamage);
        Health health = target.GetComponent<Health>();
        health.TakeDamage(type);
    }
}
