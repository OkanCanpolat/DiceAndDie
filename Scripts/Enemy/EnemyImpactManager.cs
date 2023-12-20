
public class EnemyImpactManager : ImpactManager
{
    private void Awake()
    {
        CombatManager.Instance.OnEnemyTurn += OnTurnCome;
        CombatManager.Instance.OnPlayerTurn += OnTurnEnd;
    }

    private void OnDestroy()
    {
        CombatManager.Instance.OnEnemyTurn -= OnTurnCome;
        CombatManager.Instance.OnPlayerTurn -= OnTurnEnd;
    }
}
