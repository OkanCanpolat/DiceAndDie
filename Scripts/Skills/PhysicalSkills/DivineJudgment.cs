using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "Divine Judgmnet", menuName = "Skills/Common/Divine Judgmnet")]
public class DivineJudgment : Skill
{
    [SerializeField] private GameObject effectPrefab;
    [SerializeField] private float damage;
    [SerializeField] private int damageDelay;
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
        GameObject skill = Instantiate(effectPrefab, target.transform.position + Vector3.up, Quaternion.identity);
        Destroy(skill, skillCompleteTime);
        Damage();
    }
    private async Task Damage()
    {
        await Task.Delay(damageDelay * 1000);
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
