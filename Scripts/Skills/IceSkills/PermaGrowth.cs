using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "PermaGrowth", menuName = "Skills/Rare/Perma Growth")]

public class PermaGrowth : Skill
{
    [SerializeField] private GameObject effectPrefab;
    [SerializeField] private int manaGainCount;
    [SerializeField] private float effectDestroyTime;
    [SerializeField] private int manaGainDelayMS;
    private PlayerSkillManager player;

    public override void Use()
    {
        player = CombatManager.Instance.GetPlayer();
        GameObject skill = Instantiate(effectPrefab, player.transform.position, Quaternion.identity);
        Destroy(skill, effectDestroyTime);
        GainMana();
    }
    private async Task GainMana()
    {
        await Task.Delay(manaGainDelayMS);
        player.IncreaseLastTurnManaCount(manaGainCount);
    }
}
