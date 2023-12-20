using UnityEngine;
using System.Threading.Tasks;

[CreateAssetMenu(fileName = "MagicMissile", menuName = "Skills/Enemy/Magic Missile")]

public class MagicMissile : Skill
{
    [SerializeField] private GameObject skillEffect;
    [SerializeField] private int damageDelayMS;
    [SerializeField] private float effectLifetime;
    [SerializeField] private float damage;
    private PlayerSkillManager player;
    private Resistences playerResistence;
    private Health playerHealth;
    private DamageType damageType;
    public override void Use()
    {
        player = CombatManager.Instance.GetPlayer();
        playerResistence = player.GetComponent<Resistences>();
        playerHealth = player.GetComponent<Health>();
        GameObject effect = Instantiate(skillEffect, player.transform.position, Quaternion.identity);
        Destroy(effect, effectLifetime);
        Damage();
    }

    private async Task Damage()
    {
        await Task.Delay(damageDelayMS);
        damageType = new ElectricDamage(playerResistence, damage);
        playerHealth.TakeDamage(damageType);
    }
}
