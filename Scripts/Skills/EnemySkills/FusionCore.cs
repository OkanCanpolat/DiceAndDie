using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "FusionCore", menuName = "Skills/Enemy/Fusion Core")]

public class FusionCore : Skill
{
    [SerializeField] private GameObject skillEffect;
    [SerializeField] private int turnCount;
    [SerializeField] private int damageDelayMS;
    [SerializeField] private float effectLifetime;
    [SerializeField] private float damage;
    [SerializeField] private int damageMultiplier;
    [SerializeField] private Sprite impactSprite;
    [SerializeField] private GameObject impactDescription;
    private ImpactManager impacts;
    private PlayerSkillManager player;
    private Resistences playerResistence;
    private Health playerHealth;
    private EnemyHealth enemyHealth;
    private DamageType damageType;
    public override void Use()
    {
        player = CombatManager.Instance.GetPlayer();
        playerResistence = player.GetComponent<Resistences>();
        playerHealth = player.GetComponent<Health>();
        enemyHealth = CombatManager.Instance.GetEnemy().GetComponent<EnemyHealth>();
        impacts = CombatManager.Instance.GetPlayer().GetComponent<ImpactManager>();
        SkinnedMeshRenderer meshRenderer = player.GetComponentInChildren<SkinnedMeshRenderer>();
        float offset = meshRenderer.bounds.size.y;
        Vector3 newPosition = new Vector3(player.transform.position.x, player.transform.position.y + offset, player.transform.position.z);
        GameObject effect = Instantiate(skillEffect, newPosition, Quaternion.identity);
        Destroy(effect, effectLifetime);

        ApplyFusionAndDealDamage();
    }

    private void ApplyFusionAndDealDamage()
    {
        FusionCoreImpact impact = impacts.TryGetImpact<FusionCoreImpact>(typeof(FusionCoreImpact));

        if (impact != null)
        {
            float finalDamage;
            finalDamage = damage * damageMultiplier;
            impacts.RemovePassiveImpact(impact);
            enemyHealth.RestoreHealth(finalDamage);
            Damage(finalDamage);
        }

        else
        {
            impact = new FusionCoreImpact(turnCount, impacts, impactSprite);
            impact.Init(damageMultiplier);
            impacts.AddPassiveImpact(impact, impactDescription, ImpactType.PlayerDebuff);
            Damage(damage);
        }
    }
    private async Task Damage(float damage)
    {
        await Task.Delay(damageDelayMS);
        damageType = new BleedDamage(playerResistence, damage);
        playerHealth.TakeDamage(damageType);
    }


}
