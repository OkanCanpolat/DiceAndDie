using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "StormBeacon", menuName = "Skills/Epic/Storm Beacon")]
public class StormBeacon : Skill
{
    [SerializeField] private GameObject effectPrefab;
    [SerializeField] private float effectDestroyTime;
    [SerializeField] private int damageCount;
    [SerializeField] private float damagePerStorm;
    [SerializeField] private int damagePlayerPercent;
    [SerializeField] private int criticalChancePercentage;
    [SerializeField] private int stormDelayMS;
    [SerializeField] private int delayBetweenStormsMS;
    [SerializeField] private Color environmentColor;

    private Enemy enemy;
    private PlayerSkillManager player;
    private PlayerHealth playerHealth;
    private EnemyHealth enemyHealth;
    private DamageType damageType;
    private Resistences playerResistence;
    private Resistences enemyResistence;
    private ImpactManager playerImpactManager;
    private ImpactManager enemyImpactManager;

    public override void Use()
    {
        player = CombatManager.Instance.GetPlayer();
        enemy = CombatManager.Instance.GetEnemy();
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = enemy.GetComponent<EnemyHealth>();
        playerResistence = player.GetComponent<Resistences>();
        enemyResistence = enemy.GetComponent<Resistences>();
        playerImpactManager = player.GetComponent<ImpactManager>();
        enemyImpactManager = enemy.GetComponent<ImpactManager>();

        Vector3 direction = (enemy.transform.position - player.transform.position).normalized;
        float magnitude = (enemy.transform.position - player.transform.position).magnitude;
        Vector3 position = player.transform.position + (direction * (magnitude / 2));

        GameObject skill = Instantiate(effectPrefab, position, Quaternion.identity);
        Destroy(skill, skillCompleteTime);
        GenerateStorm();
    }
    private async Task GenerateStorm()
    {
        DirectionLightController.Instance.ChangeLightColor(environmentColor);
        WetImpact playerImpact;
        WetImpact enemyImpact;

        int playerWetMultiplier = ControlWetMultiplier(out playerImpact, playerImpactManager);
        int enemyWetMultiplier = ControlWetMultiplier(out enemyImpact, enemyImpactManager);

        bool playerIsWet = playerImpact != null;
        bool enemyIsWet = enemyImpact != null;
        bool playerTakedDamage = false;
        bool enemyTakedDamage = false;

        await Task.Delay(stormDelayMS);

        for (int i = 0; i < damageCount; i++)
        {
            int result = Random.Range(0, 100);
            int critMultiplier = CalculateCrit();

            if (result < damagePlayerPercent)
            {
                damageType = new ElectricDamage(playerResistence, damagePerStorm * critMultiplier * playerWetMultiplier);
                playerHealth.TakeDamage(damageType);
                playerTakedDamage = true;
            }

            else
            {
                damageType = new ElectricDamage(enemyResistence, damagePerStorm * critMultiplier * enemyWetMultiplier);
                enemyHealth.TakeDamage(damageType);
                enemyTakedDamage = true;
            }

            await Task.Delay(delayBetweenStormsMS);
        }

        if(playerIsWet && playerTakedDamage)
        {
            playerImpactManager.RemovePassiveImpact(playerImpact);
        }
        if(enemyIsWet && enemyTakedDamage)
        {
            enemyImpactManager.RemovePassiveImpact(enemyImpact);
        }
        DirectionLightController.Instance.ResetLightColor();
    }
    private int CalculateCrit()
    {
        int value = Random.Range(0, 100);

        if (value < criticalChancePercentage)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }
    private int ControlWetMultiplier(out WetImpact impact, ImpactManager impactManager)
    {
        impact = impactManager.TryGetImpact<WetImpact>(typeof(WetImpact));
        if (impact != null) return 2;
        return 1;
    }
}
