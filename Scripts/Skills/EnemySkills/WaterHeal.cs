using UnityEngine;

[CreateAssetMenu(fileName = "WaterHeal", menuName = "Skills/Enemy/Water Heal")]

public class WaterHeal : Skill
{
    [SerializeField] private GameObject skillEffect;
    [SerializeField] private float healAmount;
    [SerializeField] private float effectLifetime;
    private EnemyHealth health;
    private Enemy enemy;
    public override void Use()
    {
        enemy = CombatManager.Instance.GetEnemy();
        GameObject effect = Instantiate(skillEffect, enemy.transform.position, Quaternion.identity);
        Destroy(effect, effectLifetime);
        health = enemy.GetComponent<EnemyHealth>();
        health.RestoreHealth(healAmount);
    }
}

