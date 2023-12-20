
using System.Collections.Generic;

public class PlayerImpactManager : ImpactManager
{
    private void Awake()
    {
        CombatManager.Instance.OnPlayerTurn += OnTurnCome;
        CombatManager.Instance.OnEnemyTurn += OnTurnEnd;
        CombatManager.Instance.OnCombatEnding += ResetImpactsOnCombatEnd;
    }
    private void OnDestroy()
    {
        CombatManager.Instance.OnPlayerTurn -= OnTurnCome;
        CombatManager.Instance.OnEnemyTurn -= OnTurnEnd;
        CombatManager.Instance.OnCombatEnding -= ResetImpactsOnCombatEnd;
    }
    private void ResetImpactsOnCombatEnd()
    {
        foreach(Impact impact in PassiveImpacts)
        {
            impact.OnDelete();
        }
        foreach (Impact impact in DeffensiveImpacts)
        {
            impact.OnDelete();
        }
        foreach (Impact impact in AttackImpacts)
        {
            impact.OnDelete();
        }

        PassiveImpacts.Clear();
        EndTurnPassiveImpacts.Clear();
        DeffensiveImpacts.Clear();
        AttackImpacts.Clear();
    }
}
