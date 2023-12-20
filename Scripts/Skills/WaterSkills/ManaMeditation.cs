using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "ManaMeditation", menuName = "Skills/Rare/Mana Meditation")]
public class ManaMeditation : Skill
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
        player.IncreaseMana(manaGainCount);
    }

}
