using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "Rainy", menuName = "Skills/Epic/Rainy")]

public class Rainy : Skill
{
    [SerializeField] private GameObject effectPrefab;
    [SerializeField] private GameObject healEffectPrefab;
    [SerializeField] private int impactDelayMS;
    [SerializeField] private int turnCount;
    [SerializeField] private float instantHealAmount;
    [SerializeField] private float turnHealAmount;
    [SerializeField] private int enemyHealChance;
    [SerializeField] private Sprite impactSprite;
    [SerializeField] private GameObject impactDescription;

    private PlayerSkillManager player;
    private Enemy enemy;
    private PlayerHealth playerHealth;
    private EnemyHealth enemyHealth;
    private ImpactManager playerImpacts;

    public override void Use()
    {
        player = CombatManager.Instance.GetPlayer();
        enemy = CombatManager.Instance.GetEnemy();
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = enemy.GetComponent<EnemyHealth>();
        playerImpacts = player.GetComponent<ImpactManager>();

        SkinnedMeshRenderer playerRenderer = player.GetComponentInChildren<SkinnedMeshRenderer>();

        Vector3 direction = (enemy.transform.position - player.transform.position).normalized;
        float magnitude = (enemy.transform.position - player.transform.position).magnitude;
        Vector3 position = player.transform.position + (direction * (magnitude / 2));
        float skillInstantiateHeight = playerRenderer.bounds.size.y;

        Vector3 upperPosition = new Vector3(position.x, position.y + skillInstantiateHeight, position.z);
        GameObject effect = Instantiate(effectPrefab, upperPosition, effectPrefab.transform.rotation);
        ApplyImpact(effect);
    }

    private async Task ApplyImpact(GameObject rainEffect)
    {
        await Task.Delay(impactDelayMS);
        playerHealth.RestoreHealth(instantHealAmount);
        Instantiate(healEffectPrefab, player.transform.position, Quaternion.identity);

        RainyImpact impact = new RainyImpact(turnCount, playerImpacts, impactSprite);
        impact.Init(player, enemy, turnHealAmount, healEffectPrefab, rainEffect, enemyHealChance);
        playerImpacts.AddPassiveImpact(impact, impactDescription, ImpactType.PlayerBuff);
    }
}
