using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "Speed Of Electron", menuName = "Skills/Rare/Speed Of Electron")]
public class SpeedOfElectron : Skill
{
    [SerializeField] private GameObject effectPrefab;
    [SerializeField] private int drawCardCount;
    [SerializeField] private int pickDelayMS;
    [SerializeField] private int delayBetweenDrawsMS;
    [SerializeField] private float effectDestroyTime;
    private PlayerSkillManager player;
    public override void Use()
    {
        player = CombatManager.Instance.GetPlayer();
        GameObject skill = Instantiate(effectPrefab, player.transform.position, Quaternion.identity);
        Destroy(skill, skillCompleteTime);
        DrawCards();
    }
    private async Task DrawCards()
    {
        await Task.Delay(pickDelayMS);

        for (int i = 0; i < drawCardCount; i++)
        {
            player.AddSkillToSlot();
            await Task.Delay(pickDelayMS);
        }
    }
}
