using UnityEngine;

[CreateAssetMenu(fileName = "AllDamageReducerBuff", menuName = "Skills/Enemy/Damage Reducer Buff")]

public class AllDamageReducerBuff : Skill
{
    public int turnCount;
    [SerializeField] private GameObject skillEffect;
    [SerializeField] private float effectLifetime;
    [SerializeField] private float damageReducePercent;
    [SerializeField] private Sprite impactSprite;
    [SerializeField] private GameObject impactDescription;
    private ImpactManager impacts;
    public override void Use()
    {
        impacts = CombatManager.Instance.GetEnemy().GetComponent<ImpactManager>();
        GameObject effect = Instantiate(skillEffect, impacts.transform.position, Quaternion.identity);
        Destroy(effect, effectLifetime);
        GeneretaBuff();
    }

    private void GeneretaBuff()
    {
        AllDamageReducerImpact impact = new AllDamageReducerImpact(turnCount, impacts, impactSprite);
        impact.Init(damageReducePercent);
        impacts.AddDeffensiveImpact(impact, impactDescription, ImpactType.EnemyBuff);
    }

   
}
