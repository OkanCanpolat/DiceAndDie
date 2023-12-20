using UnityEngine;
[CreateAssetMenu(fileName = "AttackDamageIncrementContent", menuName = "SupriseContents/Chest/Attack Damage")]

public class AttackDamageIncrementContent : SupriseCardContent
{
    [SerializeField] private float increment;
    public override void Action()
    {
        PlayerSkillManager player = CombatManager.Instance.GetPlayer();
        PlayerAttack weapon = player.GetComponent<PlayerAttack>();
        weapon.IncreaseWeaponDamage(increment);
    }
}
