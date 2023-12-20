using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillSelectionButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject description;
    [SerializeField] private Skill skill;
    [SerializeField] private Image frameImage;
    [SerializeField] private Sprite selectionFrame;
    [SerializeField] private Sprite defaultFrame;
    [SerializeField] private TMP_Text manaText;
    private bool selected;
    public Skill Skill => skill;

    private void Awake()
    {
        manaText.text = skill.RequiredMana.ToString();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (selected)
        {
            selected = false;
            frameImage.sprite = defaultFrame;
            SkillSelectionController.Instance.UnselectSkill(this);
        }
        else if (!selected && !SkillSelectionController.Instance.IsSelectionFull())
        {
            selected = true;
            frameImage.sprite = selectionFrame;
            SkillSelectionController.Instance.SelectSkill(this);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        description.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        description.SetActive(false);
    }
}
