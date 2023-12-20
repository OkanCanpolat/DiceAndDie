using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool GameCanContinue = true;
    [SerializeField] private PlayerHealth playerHealth;
    [Header("End Game Properties")]
    [SerializeField] GameObject loseScreen;
    [SerializeField] GameObject winScreen;
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
        playerHealth.OnDie += OnPlayerDie;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void OnLevelCompleted()
    {
        GameCanContinue = false;
        winScreen.SetActive(true);
    }
    private void OnPlayerDie()
    {
        GameCanContinue = false;
        loseScreen.SetActive(true);
    }


}
