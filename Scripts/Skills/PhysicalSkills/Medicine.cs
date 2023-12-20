using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "Medicine", menuName = "Skills/Rare/Medicine")]

public class Medicine : Skill
{
    [SerializeField] private GameObject effectPrefab;
    [SerializeField] private int healCount;
    [SerializeField] private int minHealAmount;
    [SerializeField] private int maxHealAmount;
    [SerializeField] private int healDelayMS;
    [SerializeField] private int healDelayBetweenHealCountsMS;
    [SerializeField] private float effectDestroyTime;
    private PlayerSkillManager player;
    private PlayerHealth health;
    public override void Use()
    {
        player = CombatManager.Instance.GetPlayer();
        health = player.GetComponent<PlayerHealth>();
        GameObject skill = Instantiate(effectPrefab, player.transform.position, Quaternion.identity);
        Destroy(skill, skillCompleteTime);
        HealSelf();
    }

    private async Task HealSelf()
    {
        await Task.Delay(healDelayMS);

        for (int i = 0; i < healCount; i++)
        {
            int heal = Random.Range(minHealAmount, maxHealAmount + 1);
            health.RestoreHealth(heal);
            await Task.Delay(healDelayBetweenHealCountsMS);
        }
    }
}
