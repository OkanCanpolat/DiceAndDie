
using UnityEngine;

[CreateAssetMenu(fileName = "BasicAttackDamageEnhancer", menuName = "Skills/Enemy/Basic Attack Damage Enhancer")]

public class BasicAttackDamageEnhancer : Skill
{
    public int turnCount;
    [SerializeField] private GameObject skillEffect;
    [SerializeField] private float effectLifetime;
    [SerializeField] private float damageBonus;
    [SerializeField] private Sprite impactSprite;
    [SerializeField] private GameObject impactDescription;
    private ImpactManager impacts;
    private Enemy enemy;

    public override void Use()
    {
        enemy = CombatManager.Instance.GetEnemy();
        impacts = enemy.GetComponent<ImpactManager>();
        GameObject effect = Instantiate(skillEffect, impacts.transform.position, Quaternion.identity);
        Destroy(effect, effectLifetime);
        GeneretaBuff();
    }

    private void GeneretaBuff()
    {
        enemy.IncreaseAttackDamage(damageBonus);
        BasicAttackDamageEnhancerImpact impact = new BasicAttackDamageEnhancerImpact(turnCount, impacts, impactSprite);
        impact.Init(damageBonus, enemy);
        impacts.AddPassiveImpact(impact, impactDescription, ImpactType.EnemyBuff);
    }
}
