using TMPro;
using UnityEngine;

public class SkillSelectionUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text selectionCountText;

    private void Start()
    {
        SkillSelectionController.Instance.OnSelectionCountChanged += ChangeSelectionCount;
    }
    private void ChangeSelectionCount(int value)
    {
        selectionCountText.text = value.ToString();
    }
}
