using UnityEngine;

public abstract class Skill : ScriptableObject
{
    public GameObject SkillDescription;
    public Sprite SkillIcon;
    public int RequiredMana;
    public bool turnConsume;
    public string animationParameterName;
    public float skillCompleteTime;
    public abstract void Use();
}
