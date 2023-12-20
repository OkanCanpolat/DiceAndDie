using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "EssenceHarvest", menuName = "Skills/Enemy/Essence Harvest")]

public class EssenceHarvest : Skill
{
    [SerializeField] private int turnCount;
    [SerializeField] private GameObject skillEffect;
    [SerializeField] private int damageDelayMS;
    [SerializeField] private float effectLifetime;
    [SerializeField] private float damage;
    [SerializeField] private int manaStealAmount;
    [SerializeField] private Sprite impactSprite;
    [SerializeField] private GameObject impactDescription;
    private ImpactManager impacts;
    private PlayerSkillManager player;
    private Resistences playerResistence;
    private Health playerHealth;
    private DamageType damageType;
    public override void Use()
    {
        player = CombatManager.Instance.GetPlayer();
        playerResistence = player.GetComponent<Resistences>();
        playerHealth = player.GetComponent<Health>();
        impacts = CombatManager.Instance.GetPlayer().GetComponent<ImpactManager>();
        GameObject effect = Instantiate(skillEffect, player.transform.position, Quaternion.identity);
        Destroy(effect, effectLifetime);

        int playerMana = player.GetLastTurnMana();
        int newMana = playerMana - manaStealAmount;
        player.SetLastTurnMana(newMana);
        Damage();
        GeneretaHarvest();
    }

    private void GeneretaHarvest()
    {
        EssenceHarvestImpact impact = new EssenceHarvestImpact(turnCount, impacts, impactSprite);
        impact.Init(player, manaStealAmount);
        impacts.AddPassiveImpact(impact, impactDescription, ImpactType.PlayerDebuff); ;
    }

    private async Task Damage()
    {
        await Task.Delay(damageDelayMS);
        damageType = new TrueDamage(playerResistence, damage);
        playerHealth.TakeDamage(damageType);
    }
}
