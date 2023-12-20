using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SkillIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject SkillDescription;
    [SerializeField] TMP_Text manaText;
    [SerializeField] Image skillImage;
    private SkillSlot slot;
    private CombatUIManager combatUIManager;

    private void Start()
    {
        RectTransform rectT = SkillDescription.GetComponent<RectTransform>();
        rectT.anchoredPosition = UIUtility.GenerateHorizontalPosition(rectT, 50f, CombatUIManager.Instance.CombatCanvas);
    }

    public void Initialize(SkillSlot slot, Sprite icon, int manaAmount, CombatUIManager manager)
    {
        this.slot = slot;
        slot.CurrentIcon = this;
        combatUIManager = manager;
        skillImage.sprite = icon;
        manaText.text = manaAmount.ToString();
    }
    public void OnClick()
    {
        if (!CombatManager.Instance.IsPlayerTurn()) return;

        if (slot.UseSkill())
        {
            slot.CurrentIcon = null;
            combatUIManager.RemoveSkillIcon(this);
            Destroy(gameObject);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SkillDescription.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SkillDescription.SetActive(false);
    }
}
