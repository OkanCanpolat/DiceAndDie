using Cinemachine;
using System;
using System.Collections;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance;
    public event Action OnCombatStart;
    public event Action OnCombatEntry;
    public event Action OnCombatEnd;
    public event Action OnCombatEnding;
    public event Action OnPlayerTurn;
    public event Action OnEnemyTurn;
    public event Action OnEnemyDied;
    private CinemachineBrain cinemachineBrain;
    private Enemy enemy;
    [SerializeField] private PlayerSkillManager player;
    private bool isPlayerTurn = true;
    private void Awake()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion
        cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
    }
    public void StartCombat()
    {
        OnCombatEntry?.Invoke();
        StartCoroutine(WaitForCameraToStart());
    }
    private IEnumerator WaitForCameraToStart()
    {
        yield return null;
        yield return null;

        while (cinemachineBrain.IsBlending )
        {
            yield return null;
        }
        OnCombatStart?.Invoke();
    }
    private IEnumerator WaitForCameraToEnd()
    {
        yield return null;
        yield return null;

        while (cinemachineBrain.IsBlending)
        {
            yield return null;
        }
        OnCombatEnd?.Invoke();
    }
    public void FinishCombat()
    {
        OnCombatEnding?.Invoke();
        StartCoroutine(WaitForCameraToEnd());
    }
    public void EnemyDied()
    {
        OnEnemyDied?.Invoke();
    }
    public void SetEnemy(Enemy enemy)
    {
        this.enemy = enemy;
    }
    public Enemy GetEnemy()
    {
        return enemy;
    }
    public PlayerSkillManager GetPlayer()
    {
        return player;
    }
    public bool IsPlayerTurn()
    {
        return isPlayerTurn;
    }
    public void SetIsPlayerTurn(bool value)
    {
        isPlayerTurn = value;
    }
    public void StartEnemyTurn()
    {
        OnEnemyTurn?.Invoke();
    }
    public void StartPlayerTurn()
    {
        SetIsPlayerTurn(true);
        OnPlayerTurn?.Invoke();
    }
}
