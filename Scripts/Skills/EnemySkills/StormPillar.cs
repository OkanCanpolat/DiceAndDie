using UnityEngine;

[CreateAssetMenu(fileName = "StormPillar", menuName = "Skills/Enemy/Storm Pillar")]

public class StormPillar : Skill
{
    [SerializeField] private GameObject skillEffect;
    [SerializeField] private int turnCount;
    [SerializeField] private int mirrorDamagePercent;
    [SerializeField] private Sprite impactSprite;
    [SerializeField] private GameObject impactDescription;
    private Enemy enemy;
    private PlayerSkillManager player;
    private ImpactManager impacts;
    private Resistences playerResistence;
    private Health playerHealth;

    public override void Use()
    {
        player = CombatManager.Instance.GetPlayer();
        enemy = CombatManager.Instance.GetEnemy();
        impacts = enemy.GetComponent<ImpactManager>();
        playerHealth = player.GetComponent<Health>();
        playerResistence = player.GetComponent<Resistences>();

        Vector3 direction = (enemy.transform.position - player.transform.position).normalized;
        float magnitude = (enemy.transform.position - player.transform.position).magnitude;
        Vector3 position = player.transform.position + (direction * (magnitude / 2));
        GameObject effect = Instantiate(skillEffect, position, Quaternion.identity);
        GenerateImpact(effect);
    }

    private void GenerateImpact(GameObject effect)
    {
        StormPillarImpact impact = new StormPillarImpact(turnCount, impacts, impactSprite);
        impact.Init(playerResistence, playerHealth, mirrorDamagePercent, effect);
        impacts.AddDeffensiveImpact(impact, impactDescription, ImpactType.EnemyBuff);
    }

}
