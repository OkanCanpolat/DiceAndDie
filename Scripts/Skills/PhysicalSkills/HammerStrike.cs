using UnityEngine;
using System.Threading.Tasks;

[CreateAssetMenu(fileName = "HammerStrike", menuName = "Skills/Rare/Hammer Strike")]
public class HammerStrike : Skill
{
    [SerializeField] private GameObject effectPrefab;
    [SerializeField] private float damage;
    [SerializeField] private int damageDelayMS;
    [SerializeField] private float effectDestroyTime;
    [SerializeField] private int bleedChange;
    [SerializeField] private float bleedDamage;
    [SerializeField] private int turnCount;
    [SerializeField] private Sprite impactSprite;
    [SerializeField] private GameObject impactDescription;
    private DamageType damageType;
    private Enemy target;

    public override void Use()
    {
        target = CombatManager.Instance.GetEnemy();
        SkinnedMeshRenderer mesh = target.GetComponentInChildren<SkinnedMeshRenderer>();
        Vector3 center = mesh.bounds.center;
        float footsPos = mesh.bounds.extents.y;
        Vector3 targetPos = new Vector3(center.x, center.y - footsPos, center.z);
        GameObject skill = Instantiate(effectPrefab, targetPos, Quaternion.identity);

        Destroy(skill, effectDestroyTime);
        Damage();
    }

    private async Task Damage()
    {
        await Task.Delay(damageDelayMS);
        Health health = target.GetComponent<Health>();
        Resistences targetResistence = target.GetComponent<Resistences>();
        damageType = new PhysicalDamage(targetResistence, damage);
        health.TakeDamage(damageType);
        ControlImpact(health, targetResistence);
    }

    private void ControlImpact(Health health, Resistences targetResistence)
    {
        int value = Random.Range(0, 100);

        if (value < bleedChange)
        {
            ImpactManager impacts = target.GetComponent<ImpactManager>();

            BleedImpact bleedImpact = new BleedImpact(turnCount, impacts, impactSprite);
            bleedImpact.Init(health, bleedDamage, targetResistence);
            impacts.AddPassiveImpact(bleedImpact, impactDescription, ImpactType.EnemyDebuff);
        }
    }
}
