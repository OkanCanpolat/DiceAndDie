using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "LaserCannon", menuName = "Skills/Epic/Laser Cannon")]
public class LaserCannon : Skill
{
    [SerializeField] private GameObject effectPrefab;
    [SerializeField] private float damagePerLaser;
    [SerializeField] private float bleedDamagePerLaser;
    [SerializeField] private int bleedChance;
    [SerializeField] private int damageDelayMS;
    [SerializeField] private int damageDelayBetweenLaserMS;
    [SerializeField] private int bleedTurnCount;
    [SerializeField] private float effectDestroyTime;
    [SerializeField] private Sprite impactSprite;
    [SerializeField] private GameObject impactDescription;
    private int laserCount = 12;
    private DamageType damageType;
    private Enemy target;

    public override void Use()
    {
        target = CombatManager.Instance.GetEnemy();
        GameObject skill = Instantiate(effectPrefab, target.transform.position, Quaternion.identity);

        Destroy(skill, effectDestroyTime);
        Damage();
    }

    private async Task Damage()
    {
        await Task.Delay(damageDelayMS);
        Health health = target.GetComponent<Health>();
        Resistences targetResistence = target.GetComponent<Resistences>();
        damageType = new PhysicalDamage(targetResistence, damagePerLaser);
        int bleedCount = 0;

        for (int i = 0; i < laserCount; i++)
        {
            int value = Random.Range(0, 100);
            if(value < bleedChance)
            {
                bleedCount++;
            }

            damageType.SetDamage(damagePerLaser);
            health.TakeDamage(damageType);

            await Task.Delay(damageDelayBetweenLaserMS);
        }

        if (bleedCount == 0) return;

        float bleedDamage = bleedCount * bleedDamagePerLaser;
        ImpactManager impacts = target.GetComponent<ImpactManager>();
        BleedImpact bleedImpact = new BleedImpact(bleedTurnCount, impacts, impactSprite);
        bleedImpact.Init(health, bleedDamage, targetResistence);
        impacts.AddPassiveImpact(bleedImpact, impactDescription, ImpactType.EnemyDebuff);
    }
}
