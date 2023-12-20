using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "SkillSelectionSaveFile")]
public class SkillSelectionSaveFile : ScriptableObject
{
    public List<Skill> SelectedSkills;
}
