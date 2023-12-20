using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "GasBomb", menuName = "Skills/Rare/Gas Bomb")]

public class GasBomb : Skill
{
    [SerializeField] private GameObject effectPrefab;
    [SerializeField] private GameObject poisoningEffectPrefab;
    [SerializeField] private int impactDelayMS;
    [SerializeField] private float poisonDamage;
    [SerializeField] private int turnCount;
    [SerializeField] private int poisoningTurnCount;
    [SerializeField] private int poisoningDoubleChance;
    [SerializeField] private int playerPoisoningStackChance;
    [SerializeField] private int enemyPoisoningStackChance;
    [SerializeField] private Sprite gasBombImpactSprite;
    [SerializeField] private GameObject gasBombImpactDescription;
    [SerializeField] private Sprite poisoningImpactSprite;
    [SerializeField] private GameObject poisoningImpactDescription;

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

    private async Task ApplyImpact(GameObject gasEffect)
    {
        await Task.Delay(impactDelayMS);

        GasBombImpact impact = new GasBombImpact(turnCount, playerImpacts, gasBombImpactSprite);
        impact.Init(gasEffect);
        playerImpacts.AddPassiveImpact(impact, gasBombImpactDescription, ImpactType.PlayerBuff);

        PoisoningImpact playerPoisioningImpact = new PoisoningImpact(poisoningTurnCount, playerImpacts, poisoningImpactSprite);
        Vector3 pos = player.GetComponentInChildren<SkinnedMeshRenderer>().bounds.center;
        playerPoisioningImpact.Init(playerHealth, playerResistence, poisoningEffectPrefab, pos);
        playerPoisioningImpact.InitPoisionValues(playerPoisoningStackChance, turnCount, poisoningDoubleChance, poisonDamage);
        playerImpacts.AddPassiveImpact(playerPoisioningImpact, poisoningImpactDescription, ImpactType.PlayerDebuff);

        PoisoningImpact enemyPoisioningImpact = new PoisoningImpact(poisoningTurnCount, enemyImpacts, poisoningImpactSprite);
        Vector3 posEnemy = enemy.GetComponentInChildren<SkinnedMeshRenderer>().bounds.center;
        enemyPoisioningImpact.Init(enemyHealth, enemyResistence, poisoningEffectPrefab, posEnemy);
        enemyPoisioningImpact.InitPoisionValues(enemyPoisoningStackChance, turnCount, poisoningDoubleChance, poisonDamage);
        enemyImpacts.AddPassiveImpact(enemyPoisioningImpact, poisoningImpactDescription, ImpactType.EnemyDebuff);
    }
}
