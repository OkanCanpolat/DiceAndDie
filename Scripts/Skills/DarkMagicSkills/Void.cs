using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "Void", menuName = "Skills/Rare/Void")]

public class Void : Skill
{
    [SerializeField] private GameObject effectPrefab;
    [SerializeField] private float effectDestroyTime;
    [SerializeField] private int skillUsageDelayMS;
    [SerializeField] private float skillInstantiateHeight;
    private PlayerSkillManager player;
    private Enemy enemy;

    public override void Use()
    {
        player = CombatManager.Instance.GetPlayer();
        enemy = CombatManager.Instance.GetEnemy();

        Vector3 direction = (enemy.transform.position - player.transform.position).normalized;
        float magnitude = (enemy.transform.position - player.transform.position).magnitude;
        Vector3 position = player.transform.position + (direction * (magnitude / 2));
        Vector3 upperPosition = new Vector3(position.x, position.y + skillInstantiateHeight, position.z);
        GameObject effect = Instantiate(effectPrefab, upperPosition, Quaternion.identity);
        Destroy(effect, effectDestroyTime);
        UseSkill();
    }

    private async Task UseSkill()
    {
        await Task.Delay(skillUsageDelayMS);
        PlayerSkillManager playerSkills = player.GetComponent<PlayerSkillManager>();
        List<SkillSlot> slots = playerSkills.SkillSlots;
        List<SkillSlot> filledSlots = new List<SkillSlot>();

        foreach(SkillSlot slot in slots)
        {
            if (!slot.IsEmpty())
            {
                filledSlots.Add(slot);
            }
        }

        int index = Random.Range(0, filledSlots.Count);
        SkillSlot selectedSlot = filledSlots[index];
        int CurrentSkillmana = selectedSlot.CurrentSkill.RequiredMana;
        selectedSlot.CurrentSkill.RequiredMana = 0;
        selectedSlot.CurrentIcon.OnClick();
        selectedSlot.CurrentSkill.RequiredMana = CurrentSkillmana;
    }

}
