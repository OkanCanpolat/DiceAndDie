using UnityEngine;

[CreateAssetMenu(fileName = "FireWeaponBuff", menuName = "Skills/Epic/Fire Weapon Buff")]

public class FireWeaponBuff : Skill
{
    [SerializeField] private GameObject Prefab;
    [SerializeField] private float weaponDamageBonus;
    [SerializeField] private float burnDamage;
    [SerializeField] private float burnChance;
    [SerializeField] private int burnTurnCount;
    [SerializeField] private int buffTurnCount;
    [SerializeField] private GameObject weaponBuff;
    [SerializeField] private Sprite burnImpactSprite;
    [SerializeField] private GameObject burnImpactDescription;
    [SerializeField] private Sprite buffImpactSprite;
    [SerializeField] private GameObject buffImpactDescription;

    private PlayerSkillManager player;
    private Enemy target;
    public override void Use()
    {
        target = CombatManager.Instance.GetEnemy();
        player = CombatManager.Instance.GetPlayer();
        RFX1_AnimatorEvents events = player.GetComponent<RFX1_AnimatorEvents>();
        events.Effect2.Prefab = Prefab;
        PlayerAttack playerWeapon = player.GetComponent<PlayerAttack>();
        GameObject buffEffect = Instantiate(weaponBuff);
        WeaponEffectCreator effect = buffEffect.GetComponent<WeaponEffectCreator>();
        effect.Mesh = playerWeapon.WeaponMesh;
        effect.Initialize();
        CreateImpact(playerWeapon, buffEffect);
    }

    private void CreateImpact(PlayerAttack playerWeapon, GameObject effect)
    {
        ImpactManager impacts = player.GetComponent<ImpactManager>();
        Health enemyHealth = target.GetComponent<Health>();
        Resistences resistence = target.GetComponent<Resistences>();
        BasicAttackBurnDecorator decorator = new BasicAttackBurnDecorator(resistence, playerWeapon.GetWeaponDamage(), burnDamage, burnChance, burnTurnCount, weaponDamageBonus);
        decorator.Init(burnImpactDescription, enemyHealth, burnImpactSprite);
        playerWeapon.SetDamageType(decorator);

        BurningWeaponImpact impact = new BurningWeaponImpact(buffTurnCount, impacts, buffImpactSprite);
        impact.Init(effect, playerWeapon, weaponDamageBonus);
        impacts.AddPassiveImpact(impact, buffImpactDescription, ImpactType.PlayerBuff); ;
    }
}
