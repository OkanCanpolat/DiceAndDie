using UnityEngine;

[CreateAssetMenu(fileName = "SelfDamageContent", menuName = "SupriseContents/Self Damage")]
public class SelfDamageContent : SupriseCardContent
{
    [SerializeField] private float damageAmount;
    public override void Action()
    {
        PlayerSkillManager player = CombatManager.Instance.GetPlayer();
        PlayerHealth health = player.GetComponent<PlayerHealth>();
        Resistences resistence = player.GetComponent<Resistences>();
        health.TakeDamage(new TrueDamage(resistence, damageAmount));
    }
}
