using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ImpactIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image impactImage;
    [SerializeField] private GameObject descriptionPanel;

    private void Start()
    {
        RectTransform rectT = descriptionPanel.GetComponent<RectTransform>();
        rectT.anchoredPosition = UIUtility.GenerateHorizontalPosition(rectT, 50f, CombatUIManager.Instance.CombatCanvas);
    }

    public void Initialize(Sprite sprite, GameObject description)
    {
        impactImage.sprite = sprite;
        descriptionPanel = description;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        descriptionPanel.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionPanel.SetActive(false);
    }

}
