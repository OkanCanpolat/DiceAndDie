using UnityEngine;
using System.Threading.Tasks;

[CreateAssetMenu(fileName = "Snowy", menuName = "Skills/Epic/Snowy")]
public class Snowy : Skill
{
    [SerializeField] private GameObject effectPrefab;
    [SerializeField] private GameObject snowExplosionEffectPrefab;
    [SerializeField] private GameObject snowBigExplosionEffectPrefab;
    [SerializeField] private int impactDelayMS;
    [SerializeField] private int turnCount;
    [SerializeField] private float explosionDamage;
    [SerializeField] private float bigExplosionDamage;
    [SerializeField] private int stunChance;
    [SerializeField] private int explosionEachXTurn;
    [SerializeField] private int playerFreezeChance;
    [SerializeField] private int enemyFreezeChance;
    [SerializeField] private int stunTurnCount;
    [SerializeField] private Sprite snowyImpactSprite;
    [SerializeField] private GameObject snowyImpactDescription;
    [SerializeField] private Sprite freezingImpactSprite;
    [SerializeField] private GameObject freezingImpactDescription;
    [SerializeField] private Sprite stunImpactSprite;
    [SerializeField] private GameObject stunImpactDescription;


    private PlayerSkillManager player;
    private Enemy enemy;
    private PlayerHealth playerHealth;
    private EnemyHealth enemyHealth;
    private ImpactManager playerImpacts;
    private ImpactManager enemyImpacts;
    private Resistences playerResistence;
    private Resistences enemyResistence;

    public override void Use()
    {
        player = CombatManager.Instance.GetPlayer();
        enemy = CombatManager.Instance.GetEnemy();
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = enemy.GetComponent<EnemyHealth>();
        playerImpacts = player.GetComponent<ImpactManager>();
        enemyImpacts = enemy.GetComponent<ImpactManager>();
        playerResistence = player.GetComponent<Resistences>();
        enemyResistence = enemy.GetComponent<Resistences>();

        SkinnedMeshRenderer playerRenderer = player.GetComponentInChildren<SkinnedMeshRenderer>();

        Vector3 direction = (enemy.transform.position - player.transform.position).normalized;
        float magnitude = (enemy.transform.position - player.transform.position).magnitude;
        Vector3 position = player.transform.position + (direction * (magnitude / 2));
        float skillInstantiateHeight = playerRenderer.bounds.size.y;

        Vector3 upperPosition = new Vector3(position.x, position.y + skillInstantiateHeight, position.z);
        GameObject effect = Instantiate(effectPrefab, upperPosition, effectPrefab.transform.rotation);
        ApplyImpact(effect);
    }

    private async Task ApplyImpact(GameObject snowEffect)
    {
        await Task.Delay(impactDelayMS);

        SnowyImpact impact = new SnowyImpact(turnCount, playerImpacts, snowyImpactSprite);
        impact.Init(snowEffect);
        playerImpacts.AddPassiveImpact(impact, snowyImpactDescription, ImpactType.PlayerBuff);

        FreezingImpact playerFreezingImpact = new FreezingImpact(turnCount, playerImpacts, freezingImpactSprite);
        Vector3 pos = player.GetComponentInChildren<SkinnedMeshRenderer>().bounds.center;
        playerFreezingImpact.InitExplosionDamages(explosionDamage, bigExplosionDamage);
        playerFreezingImpact.InitStunProperties(stunTurnCount, stunImpactSprite, stunImpactDescription, stunChance, ImpactType.PlayerDebuff);
        playerFreezingImpact.Init(playerHealth, playerResistence, snowExplosionEffectPrefab, snowBigExplosionEffectPrefab, playerFreezeChance, explosionEachXTurn, pos);
        playerImpacts.AddPassiveImpact(playerFreezingImpact, freezingImpactDescription, ImpactType.PlayerDebuff);

        FreezingImpact enemyFreezingImpact = new FreezingImpact(turnCount, enemyImpacts, freezingImpactSprite);
        Vector3 posEnemy = enemy.GetComponentInChildren<SkinnedMeshRenderer>().bounds.center;
        enemyFreezingImpact.InitExplosionDamages(explosionDamage, bigExplosionDamage);
        enemyFreezingImpact.InitStunProperties(stunTurnCount, stunImpactSprite, stunImpactDescription, stunChance, ImpactType.EnemyDebuff);
        enemyFreezingImpact.Init(enemyHealth, enemyResistence, snowExplosionEffectPrefab, snowBigExplosionEffectPrefab, enemyFreezeChance, explosionEachXTurn, posEnemy);
        enemyImpacts.AddPassiveImpact(enemyFreezingImpact, freezingImpactDescription, ImpactType.EnemyDebuff);
    }
}
