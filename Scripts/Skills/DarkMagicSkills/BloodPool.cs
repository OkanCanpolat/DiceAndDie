using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "BloodPool", menuName = "Skills/Legendary/Blood Pool")]

public class BloodPool : Skill
{
    [SerializeField] private GameObject Prefab;
    [SerializeField] private float selfHeal;
    [SerializeField] private float damage;
    [SerializeField] private int lifeStealChance;
    [SerializeField] private GameObject damageEffect;

    private DamageType type;
    private PlayerSkillManager player;
    private PlayerHealth playerHealth;
    private Enemy target;
    private int damageDelay = 3000;
    public override void Use()
    {
        player = CombatManager.Instance.GetPlayer();
        RFX1_AnimatorEvents events = player.GetComponent<RFX1_AnimatorEvents>();
        events.Effect2.Prefab = Prefab;
        target = CombatManager.Instance.GetEnemy();
        playerHealth = player.GetComponent<PlayerHealth>();
        playerHealth.RestoreHealth(selfHeal);
        DealDamage();
    }
    private async Task DealDamage()
    {
        await Task.Delay(damageDelay);
        Health enemyHealth = target.GetComponent<Health>();
        Resistences enemyResistences = target.GetComponent<Resistences>();
        type = new TrueDamage(enemyResistences, damage);
        enemyHealth.TakeDamage(type);

        Renderer enemyRenderer = target.GetComponentInChildren<Renderer>();
        Vector3 targetPos = enemyRenderer.bounds.center;
        GameObject effect = Instantiate(damageEffect, targetPos, Quaternion.identity);
        Destroy(effect, 1);
        int value = Random.Range(0, 100);

        if (value < lifeStealChance)
        {
            playerHealth.RestoreHealth(damage);
        }
    }
}
