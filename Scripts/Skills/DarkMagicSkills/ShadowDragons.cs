using UnityEngine;

[CreateAssetMenu(fileName = "ShadowDragons", menuName = "Skills/Rare/Shadow Dragons")]
public class ShadowDragons : Skill
{
    [SerializeField] private GameObject shotPrefab;
    [SerializeField] private GameObject buffPrefab;
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private float trueDamage;
    [SerializeField] private float selfHeal;
    [SerializeField] private float selfHealChange = 50f;

    private DamageType type;
    private PlayerSkillManager player;
    private Enemy target;

    public override void Use()
    {
        target = CombatManager.Instance.GetEnemy();
        player = CombatManager.Instance.GetPlayer();
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
        TrySelfHeal();
    }
    private void TrySelfHeal()
    {
        int change = Random.Range(0, 100);

        if(change < selfHealChange)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            playerHealth.RestoreHealth(selfHeal);
        }
    }
}
