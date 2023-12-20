using UnityEngine;
[CreateAssetMenu(fileName = "ArrowPortal", menuName = "Skills/Epic/Arrow Portal")]

public class ArrowPortal : Skill
{
    [SerializeField] private GameObject shotPrefab;
    [SerializeField] private GameObject buffPrefab;
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private float trueDamage;
    [SerializeField] private float selfDamage;
    [SerializeField] private float criticalChange;

    private DamageType type;
    private PlayerSkillManager player;
    private Enemy target;
    public override void Use()
    {
        player = CombatManager.Instance.GetPlayer();
        Health playerHealth = player.GetComponent<Health>();
        Resistences playerResistence = player.GetComponent<Resistences>();
        type = new TrueDamage(playerResistence, selfDamage);
        playerHealth.TakeDamage(type);
        target = CombatManager.Instance.GetEnemy();
        AE_AnimatorEvents events = player.GetComponent<AE_AnimatorEvents>();
        events.Effect1.Prefab = shotPrefab;
        events.Effect2.Prefab = weaponPrefab;
        events.Effect3.Prefab = buffPrefab;
    }

    public void OnCollision()
    {
        Health health = target.GetComponent<Health>();
        Resistences enemyResistences = target.GetComponent<Resistences>();
        type = new TrueDamage(enemyResistences, trueDamage);


        health.TakeDamage(type);

        if (TestCriticalStrike())
        {
            health.TakeDamage(type);
        }
    }
    private bool TestCriticalStrike()
    {
        float value = Random.Range(0, 100);
        if(value < criticalChange)
        {
            return true;
        }
        return false;
    }
}
