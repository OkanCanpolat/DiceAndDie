using UnityEngine;
using System.Threading.Tasks;

[CreateAssetMenu(fileName = "IonStrike", menuName = "Skills/Rare/Ion Strike")]
public class IonStrike : Skill
{
    
    [SerializeField] private GameObject effectPrefab;
    [SerializeField] private float iceDamage;
    [SerializeField] private int damageDelayMS;
    [SerializeField] private float effectDestroyTime;
    [SerializeField] private int freezeTurnCount;
    [SerializeField] private Sprite stunImpactSprite;
    [SerializeField] private GameObject stunImpactDescription;
    [SerializeField] private Sprite freezeImpactSprite;
    [SerializeField] private GameObject freezeImpactDescription;

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
        ImpactManager impacts = target.GetComponent<ImpactManager>();
        float damage = ControlFreezeDamage(impacts);
        damageType = new IceDamage(targetResistence, damage);
        health.TakeDamage(damageType);
    }

    private float ControlFreezeDamage(ImpactManager impacts)
    {
        int damageMultiplier = 1;

        FreezeImpact freeze = impacts.TryGetImpact<FreezeImpact>(typeof(FreezeImpact));

        if (freeze != null)
        {
            damageMultiplier = 2;
        }

        else
        {
            FreezeImpact freezeImpact = new FreezeImpact(freezeTurnCount, impacts, freezeImpactSprite);
            impacts.AddPassiveImpact(freezeImpact, freezeImpactDescription, ImpactType.EnemyDebuff);
        }

        return iceDamage * damageMultiplier;
    }
}
