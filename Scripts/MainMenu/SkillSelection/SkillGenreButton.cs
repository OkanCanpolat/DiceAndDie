
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillGenreButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private GameObject description;
    [SerializeField] private GameObject skills;
    [SerializeField] private RectTransform rect;
    private float widthChangeAmount = 20;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        description.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        description.SetActive(false);

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SkillGenreButtonController.Instance.OnButtonClick(this);
    }
    public void OnSelect()
    {
        rect.sizeDelta = new Vector3(rect.sizeDelta.x + widthChangeAmount,
                                     rect.sizeDelta.y);
        skills.SetActive(true);
    }
    public void OnDeselect()
    {
        rect.sizeDelta = new Vector3(rect.sizeDelta.x - widthChangeAmount,
                                     rect.sizeDelta.y);
        skills.SetActive(false);
    }
}
