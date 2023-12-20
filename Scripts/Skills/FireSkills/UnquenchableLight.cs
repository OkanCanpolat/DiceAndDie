using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "UnquenchableLight", menuName = "Skills/Epic/Unquenchable Light")]

public class UnquenchableLight : Skill
{
    [SerializeField] private GameObject sunLightPrefab;
    [SerializeField] private GameObject healEffectPrefab;
    [SerializeField] private GameObject damageEffectPrefab;
    [SerializeField] private GameObject explosionEffectPrefab;
    [SerializeField] private int turnCount;
    [SerializeField] private int impactDelayMS;
    [SerializeField] private int minDamage;
    [SerializeField] private int maxDamage;
    [SerializeField] private int minWithBurnDamage;
    [SerializeField] private int maxWithBurnDamage;
    [SerializeField] private int missingHealPercentage;
    [SerializeField] private Sprite unquenchableLightImpactSprite;
    [SerializeField] private GameObject unquenchableLightImpactDescription;
    [SerializeField] private Sprite burningLightImpactSprite;
    [SerializeField] private GameObject burningLightImpactDescription;
    [SerializeField] private float effectInstantiateHeightOffset;
    private float lightChanceSpeed = 2f;

    private PlayerSkillManager player;
    private Enemy enemy;
    private PlayerHealth playerHealth;
    private EnemyHealth enemyHealth;
    private ImpactManager playerImpacts;
    private ImpactManager enemyImpacts;
    private Resistences enemyResistence;

    public override void Use()
    {
        player = CombatManager.Instance.GetPlayer();
        enemy = CombatManager.Instance.GetEnemy();
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = enemy.GetComponent<EnemyHealth>();
        playerImpacts = player.GetComponent<ImpactManager>();
        enemyImpacts = enemy.GetComponent<ImpactManager>();
        enemyResistence = enemy.GetComponent<Resistences>();

        SkinnedMeshRenderer playerRenderer = player.GetComponentInChildren<SkinnedMeshRenderer>();

        Vector3 direction = (enemy.transform.position - player.transform.position).normalized;
        float magnitude = (enemy.transform.position - player.transform.position).magnitude;
        Vector3 position = player.transform.position + (direction * (magnitude / 2));
        float skillInstantiateHeight = playerRenderer.bounds.size.y + effectInstantiateHeightOffset;

        Vector3 upperPosition = new Vector3(position.x, position.y + skillInstantiateHeight, position.z);
        GameObject effect = Instantiate(sunLightPrefab, upperPosition, sunLightPrefab.transform.rotation);


        ApplyImpact(effect);
    }

    private async Task ApplyImpact(GameObject sunLightEffect)
    {
        float directionLightIntensity = DirectionLightController.Instance.GetIntensity();
        DirectionLightController.Instance.ChangeLightIntensity(lightChanceSpeed, -directionLightIntensity);

        await Task.Delay(impactDelayMS);

        UnquencableLightImpact impact = new UnquencableLightImpact(turnCount, playerImpacts, unquenchableLightImpactSprite);
        Vector3 healEffectPos = player.GetComponentInChildren<SkinnedMeshRenderer>().bounds.center;
        impact.Init(sunLightEffect, healEffectPrefab, directionLightIntensity, missingHealPercentage ,playerHealth, healEffectPos);
        playerImpacts.AddPassiveImpact(impact, unquenchableLightImpactDescription, ImpactType.PlayerBuff);

        BurningLightImpact enemyImpact = new BurningLightImpact(turnCount, enemyImpacts, burningLightImpactSprite);

        Vector3 damageEffectPos = enemy.GetComponentInChildren<SkinnedMeshRenderer>().bounds.center;
        float height = enemy.GetComponentInChildren<SkinnedMeshRenderer>().bounds.extents.y;
        Vector3 enemyHead = new Vector3(damageEffectPos.x, damageEffectPos.y + height + 1f, damageEffectPos.z);
        enemyImpact.Init(damageEffectPrefab, explosionEffectPrefab, enemyHealth, enemyResistence, enemyHead);
        enemyImpact.InitDamages(minDamage, maxDamage, minWithBurnDamage, maxWithBurnDamage);
        enemyImpacts.AddPassiveImpact(enemyImpact, burningLightImpactDescription, ImpactType.EnemyDebuff);
    }
}
