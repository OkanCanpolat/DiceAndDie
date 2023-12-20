using UnityEngine;
using System.Threading.Tasks;

[CreateAssetMenu(fileName = "SpiritBomb", menuName = "Skills/Enemy/Spirit Bomb")]
public class SpiritBomb : Skill
{
    [SerializeField] private GameObject skillEffect;
    [SerializeField] private int damageDelayMS;
    [SerializeField] private float effectLifetime;
    [SerializeField] private float minDamage;
    [SerializeField] private float maxDamage;
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
        float damage = Random.Range(minDamage, maxDamage);
        damageType = new TrueDamage(playerResistence, damage);
        playerHealth.TakeDamage(damageType);
    }
}
