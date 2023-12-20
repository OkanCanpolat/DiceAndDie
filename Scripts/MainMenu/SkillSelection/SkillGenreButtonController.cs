using UnityEngine;
using System.Collections.Generic;

public class SkillGenreButtonController : MonoBehaviour
{
    public static SkillGenreButtonController Instance;
    [SerializeField] private List<SkillGenreButton> genreButtons;
    private SkillGenreButton currentClickedButton;

    private void Awake()
    {
        #region Singleton
        if(Instance == null)
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

    private void Start()
    {
        currentClickedButton = genreButtons[0];
        currentClickedButton.OnSelect();
    }

    public void OnButtonClick(SkillGenreButton button)
    {
        if (currentClickedButton == button) return;
        currentClickedButton.OnDeselect();
        currentClickedButton = button;
        currentClickedButton.OnSelect();
    }
}
