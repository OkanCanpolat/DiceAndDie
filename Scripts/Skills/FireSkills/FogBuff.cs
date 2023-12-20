using UnityEngine;

[CreateAssetMenu(fileName = "FogBuff", menuName = "Skills/Common/Fog Buff")]

public class FogBuff : Skill
{
    [SerializeField] private GameObject Prefab;
    [SerializeField] private float missChance;
    [SerializeField] private int turnCount;
    [SerializeField] private Sprite impactSprite;
    [SerializeField] private GameObject impactDescription;

    private PlayerSkillManager player;
    public override void Use()
    {
        player = CombatManager.Instance.GetPlayer();
        RFX1_AnimatorEvents events = player.GetComponent<RFX1_AnimatorEvents>();
        events.Effect2.Prefab = Prefab;
        ControlImpact();
    }

    private void ControlImpact( )
    {
        Resistences resist = player.GetComponent<Resistences>();
        ImpactManager impacts = player.GetComponent<ImpactManager>();
        FogImpact impact = new FogImpact(turnCount, impacts, impactSprite);
        impact.Init(resist, missChance);
        impacts.AddPassiveImpact(impact, impactDescription, ImpactType.PlayerBuff);
    }
}
