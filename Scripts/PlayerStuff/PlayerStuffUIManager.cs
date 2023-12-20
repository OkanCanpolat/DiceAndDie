using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStuffUIManager : MonoBehaviour
{
    [SerializeField] private PlayerStuffManager stuffManager;
    [Header ("Food Properties")]
    [SerializeField] private TMP_Text foodAmountTxt;
    [SerializeField] private Button foodButton;
    [Header("Key Properties")]
    [SerializeField] private TMP_Text keyAmountTxt;
    private void Awake()
    {
        stuffManager.OnFoodChanged += OnFoodAmountChanged;
        stuffManager.OnKeyChanged += OnKeyAmountChanged;
    }
    public void OnFoodAmountChanged(int value)
    {
        foodAmountTxt.text = value.ToString();
        if(value == 0)
        {
            foodButton.interactable = false;
        }
        else
        {
            foodButton.interactable = true;
        }
    }
    public void OnKeyAmountChanged(int value)
    {
        keyAmountTxt.text = value.ToString();
    }
}
