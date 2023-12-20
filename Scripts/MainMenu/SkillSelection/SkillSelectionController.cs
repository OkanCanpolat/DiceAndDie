using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillSelectionController : MonoBehaviour
{
    public static SkillSelectionController Instance;
    public Action<int> OnSelectionCountChanged;
    public Action OnSelectionComplete;
    public Action OnSelectionNotComplete;
    [SerializeField] private SkillSelectionSaveFile saveFile;
    private const int requiredSkillAmount = 20;
    private List<Skill> selectedSkills = new List<Skill>();
    private List<SkillSelectionButton> selectedSkillButtons = new List<SkillSelectionButton>();
    private int currentSelectedSkillCount;
    private bool isSelectionFull;
    private void Awake()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        #endregion
    }

    public void SelectSkill(SkillSelectionButton skillButton)
    {
        selectedSkillButtons.Add(skillButton);
        selectedSkills.Add(skillButton.Skill);
        currentSelectedSkillCount++;
        ControlSelectionCount();
        OnSelectionCountChanged?.Invoke(currentSelectedSkillCount);
    }
    public void UnselectSkill(SkillSelectionButton skillButton)
    {
        selectedSkillButtons.Remove(skillButton);
        selectedSkills.Remove(skillButton.Skill);
        currentSelectedSkillCount--;
        ControlSelectionCount();
        OnSelectionCountChanged?.Invoke(currentSelectedSkillCount);
    }
    public bool IsSelectionFull()
    {
        return isSelectionFull;
    }
    public void SaveSelection()
    {
        saveFile.SelectedSkills.Clear();
        foreach (Skill skill in selectedSkills)
        {
            saveFile.SelectedSkills.Add(skill);
        }

        if (saveFile.SelectedSkills.Count == requiredSkillAmount)
        {
            OnSelectionComplete?.Invoke();
        }
        else
        {
            OnSelectionNotComplete?.Invoke();
        }
    }
    public void ResetSelection()
    {
        PointerEventData pointer = new PointerEventData(EventSystem.current);

        for (int i = 0; i < selectedSkillButtons.Count;)
        {
            selectedSkillButtons[i].OnPointerClick(pointer);
        }

    }
    public bool IsSelectionComplete()
    {
        return saveFile.SelectedSkills.Count == requiredSkillAmount;
    }
    private void ControlSelectionCount()
    {
        if (currentSelectedSkillCount == requiredSkillAmount)
        {
            isSelectionFull = true;
        }

        else
        {
            isSelectionFull = false;
        }
    }

}
