using UnityEngine;

[CreateAssetMenu(fileName = "SkullBleed", menuName = "Skills/Enemy/Skull Bleed")]
public class SkullBleed : Skill
{
    [SerializeField] private int turnCount;
    [SerializeField] private float bleedDamage;
    [SerializeField] private GameObject skillEffect;
    [SerializeField] private float effectLifetime;
    [SerializeField] private Sprite impactSprite;
    [SerializeField] private GameObject impactDescription;
    private PlayerSkillManager player;
    private ImpactManager impacts;
    private Resistences playerResistence;
    private Health playerHealth;
    public override void Use()
    {
        player = CombatManager.Instance.GetPlayer();
        impacts = player.GetComponent<ImpactManager>();
        playerResistence = player.GetComponent<Resistences>();
        playerHealth = player.GetComponent<Health>();

        GameObject effect = Instantiate(skillEffect, player.transform.position, Quaternion.identity);
        Destroy(effect, effectLifetime);
        ApplyBleed();
    }

    private void ApplyBleed()
    {
        BleedImpact impact = new BleedImpact(turnCount, impacts, impactSprite);
        impact.Init(playerHealth, bleedDamage, playerResistence);
        impacts.AddPassiveImpact(impact, impactDescription, ImpactType.PlayerDebuff);
    }


}
