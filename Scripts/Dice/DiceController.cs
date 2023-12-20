using Cinemachine;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class DiceController : MonoBehaviour
{
    public static DiceController Instace;
    public event Action<int> OnDiceRoll;
    public event Action OnFreeViewActivated;
    public bool DiceEnable => diceEnable;
    public bool CanRoll => canRoll;
    public bool CanChangeView => canChangeView;


    [SerializeField] private TMP_Text rollText;
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private TMP_Text changeViewText;
    [SerializeField] private Renderer diceRenderer;
    [SerializeField] private GameObject dice;
    [SerializeField] private CameraController cameraController;
    
    private CinemachineBrain cinemachineBrain;
    private const int minDiceResult = 1;
    private const int maxDiceResult = 6;
    private int diceResult;
    private bool diceEnable;
    private bool canRoll = true;
    private bool canChangeView = true;

    [Header("Test Properies")]
    [SerializeField] private bool Enable;
    [SerializeField] private int result;

    private void Awake()
    {
        #region Singleton
        if (Instace == null)
        {
            Instace = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        #endregion

        cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
        OpenDice();
    }

    private void Start()
    {
        CombatManager.Instance.OnCombatEnd += OpenDice;
    }

    public void RollDice()
    {
        //TEST
        if (Enable)
        {
            diceEnable = false;
            canChangeView = false;
            diceResult = result;
            rollText.enabled = false;
            changeViewText.enabled = false;
            resultText.text = diceResult.ToString();
            resultText.enabled = true;
            diceRenderer.enabled = false;
            Invoke("CloseDice", 1);
            OnDiceRoll?.Invoke(diceResult);
            return;
        }
        //END TEST
        diceEnable = false;
        canChangeView = false;
        diceResult = UnityEngine.Random.Range(minDiceResult, maxDiceResult + 1);
        rollText.enabled = false;
        resultText.text = diceResult.ToString();
        resultText.enabled = true;
        changeViewText.enabled = false;
        diceRenderer.enabled = false;
        Invoke("CloseDice", 1);
        OnDiceRoll?.Invoke(diceResult);
    }

    public void CloseDice()
    {
        diceEnable = false;
        dice.SetActive(false);
    }

    public void OpenDice()
    {
        diceEnable = true;
        dice.SetActive(true);
        rollText.enabled = true;
        canRoll = true;
        canChangeView = true;
        changeViewText.enabled = true;
        resultText.enabled = false;
        diceRenderer.enabled = true;
    }
    public void SetDiceEnable(bool value)
    {
        diceEnable = value;
    }
    public void SetCanChangeView(bool value)
    {
        canChangeView = value;
    }

    public void ChangeView()
    {
        if (canRoll && canChangeView)
        {
            StartCoroutine(ChangeToFreeView());
        }

        else if(!canRoll && canChangeView)
        {
            StartCoroutine(ChangeToDiceView());
        }
    }

    private IEnumerator WaitForBlendEnd()
    {
        yield return null;
        yield return null;

        while (cinemachineBrain.IsBlending)
        {
            yield return null;
        }

        canChangeView = true;
    }

    private IEnumerator ChangeToDiceView()
    {
        cameraController.ChangeToDiceCamera();
        canChangeView = false;
        canRoll = true;
        yield return StartCoroutine(WaitForBlendEnd());
        diceEnable = true;
        rollText.enabled = true;
        diceRenderer.enabled = true;
    }

    private IEnumerator ChangeToFreeView()
    {
        OnFreeViewActivated?.Invoke();
        cameraController.ChangeToFreeCamera();
        canChangeView = false;
        diceEnable = false;
        rollText.enabled = false;
        diceRenderer.enabled = false;
        yield return StartCoroutine(WaitForBlendEnd());
        canRoll = false;
    }

}
