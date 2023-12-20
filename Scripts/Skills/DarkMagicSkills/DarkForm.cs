using UnityEngine;
using System.Threading.Tasks;

[CreateAssetMenu(fileName = "DarkForm", menuName = "Skills/Legendary/Dark Form")]
public class DarkForm : Skill
{
    [SerializeField] private GameObject Prefab;
    [SerializeField] private float selfDamage;
    [SerializeField] private Material[] materials;
    [SerializeField] private Sprite darkFormImpactSprite;
    [SerializeField] private GameObject darkFormDescription;
    [SerializeField] private Sprite curseImpactSprite;
    [SerializeField] private GameObject curseImpactDescription;
    [SerializeField] private float selfCursePercentage;
    [SerializeField] private float enemyCursePercentage;
    [SerializeField] private int turnCount;
    private DamageType type;

    private int delay = 3000;
    private PlayerSkillManager player;
    private Enemy target;
    public override void Use()
    {
        player = CombatManager.Instance.GetPlayer();
        CharacterViewChanger changer = player.GetComponent<CharacterViewChanger>();
        RFX1_AnimatorEvents events = player.GetComponent<RFX1_AnimatorEvents>();
        events.Effect2.Prefab = Prefab;
        target = CombatManager.Instance.GetEnemy();
        Health playerHealth = player.GetComponent<Health>();
        Resistences playerResistences = player.GetComponent<Resistences>();
        type = new TrueDamage(playerResistences, selfDamage);

        playerHealth.TakeDamage(type);
        PassToDarkForm(changer);
    }
    private async Task PassToDarkForm(CharacterViewChanger changer)
    {
        await Task.Delay(delay);
        changer.DeactivateClothesAndHair();
        changer.ChangeView(materials);
        CreateImpact(changer);
    }

    private void CreateImpact(CharacterViewChanger changer)
    {
        ImpactManager playerImpacts = player.GetComponent<ImpactManager>();
        ImpactManager enemyImpacts = target.GetComponent<ImpactManager>();

        DarkFormImpact darkForm = new DarkFormImpact(turnCount, playerImpacts, darkFormImpactSprite);
        darkForm.Init(selfCursePercentage, changer);
        playerImpacts.AddDeffensiveImpact(darkForm, darkFormDescription, ImpactType.PlayerDebuff);

        DeffenseCurseImpact curse = new DeffenseCurseImpact(turnCount, enemyImpacts, curseImpactSprite);
        curse.Init(enemyCursePercentage);
        enemyImpacts.AddDeffensiveImpact(curse, curseImpactDescription, ImpactType.EnemyDebuff);
    }
}
