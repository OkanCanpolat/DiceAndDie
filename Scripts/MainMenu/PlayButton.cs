using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject description;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        DeactivateButton();
    }
    private void Start()
    {
        SkillSelectionController.Instance.OnSelectionComplete += ActivateButton;
        SkillSelectionController.Instance.OnSelectionNotComplete += DeactivateButton;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!SkillSelectionController.Instance.IsSelectionComplete())
        {
            description.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (description.activeSelf)
        {
            description.SetActive(false);
        }
    }
    public void OnClick()
    {
        SceneManager.LoadScene(1);
    }
    private void ActivateButton()
    {
        button.enabled = true;
    }
    private void DeactivateButton()
    {
        button.enabled = false;
    }
}
