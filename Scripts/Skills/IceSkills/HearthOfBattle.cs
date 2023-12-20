using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "HearthOfBattle", menuName = "Skills/Legendary/Hearth Of Battle")]

public class HearthOfBattle : Skill
{
    [SerializeField] private GameObject effectPrefab;
    [SerializeField] private int turnCount;
    [SerializeField] private float healPercentage;
    [SerializeField] private float damagePercentage;
    [SerializeField] private float effectDestroyTime;
    [SerializeField] private float lightAbsorbTime;
    [SerializeField] private int lightAbsorbedTimeMS;
    [SerializeField] private float enlightenmentTime;
    [SerializeField] private GameObject enlightenmentImpactDescription;
    [SerializeField] private Sprite enlightenmentImpactSprite;
    [SerializeField] private GameObject blindImpactDescription;
    [SerializeField] private Sprite blindImpactSprite;
    private PlayerSkillManager player;
    private Enemy enemy;
    private Resistences enemyResistence;
    private ImpactManager enemyImpacts;
    private ImpactManager playerImpacts;
    private PlayerHealth playerHealth;
    private EnemyHealth enemyHealth;

    public override void Use()
    {
        player = CombatManager.Instance.GetPlayer();
        enemy = CombatManager.Instance.GetEnemy();
        enemyResistence = enemy.GetComponent<Resistences>();
        enemyImpacts = enemy.GetComponent<ImpactManager>();
        playerImpacts = player.GetComponent<ImpactManager>();
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = enemy.GetComponent<EnemyHealth>();

        Vector3 direction = (enemy.transform.position - player.transform.position).normalized;
        float magnitude = (enemy.transform.position - player.transform.position).magnitude;
        Vector3 position = player.transform.position + (direction * (magnitude / 2));
        GameObject effect = Instantiate(effectPrefab, position, Quaternion.identity);
        Destroy(effect, effectDestroyTime);

        AbsorbLight();
    }
    private async Task AbsorbLight()
    {
        float intensity = DirectionLightController.Instance.GetIntensity();
        DirectionLightController.Instance.ChangeLightIntensity(lightAbsorbTime, -intensity);
        await Task.Delay(lightAbsorbedTimeMS);

        EnlightenmentImpact enlighmentImpact = new EnlightenmentImpact(turnCount, playerImpacts, enlightenmentImpactSprite);
        enlighmentImpact.Init(playerHealth, healPercentage);
        playerImpacts.AddPassiveImpact(enlighmentImpact, enlightenmentImpactDescription, ImpactType.PlayerBuff);

        BlindingLightImpact blindImpact = new BlindingLightImpact(turnCount, enemyImpacts, blindImpactSprite);
        blindImpact.Init(enemyHealth, enemyResistence, damagePercentage);
        enemyImpacts.AddPassiveImpact(blindImpact, blindImpactDescription, ImpactType.EnemyDebuff);
        
        DirectionLightController.Instance.ChangeLightIntensity(enlightenmentTime, intensity);
    }
}
