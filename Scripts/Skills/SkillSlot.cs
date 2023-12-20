using UnityEngine;

public class SkillSlot : MonoBehaviour
{
    public Skill CurrentSkill => slotSkill;
    public SkillIcon CurrentIcon;
    [SerializeField] private PlayerSkillManager skillManager;
    private bool isEmpty = true;
    private Skill slotSkill;

    public bool IsEmpty()
    {
        return isEmpty;
    }
    public bool UseSkill()
    {
        if (skillManager.UseSkillIfValid(slotSkill))
        {
            isEmpty = true;
            return true;
        }
        return false;
    }
    public void AddSkill(Skill skill)
    {
        slotSkill = skill;
        isEmpty = false;
    }
    public void ClearSlot()
    {
        isEmpty = true;
        slotSkill = null;
    }
}
