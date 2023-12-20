using System;
using System.Collections.Generic;
using UnityEngine;

public class ImpactManager : MonoBehaviour
{
    protected List<Impact> PassiveImpacts = new List<Impact>();
    protected List<Impact> EndTurnPassiveImpacts = new List<Impact>();
    protected List<DeffensiveImpact> DeffensiveImpacts = new List<DeffensiveImpact>();
    protected List<AttackImpact> AttackImpacts = new List<AttackImpact>();

    public List<DeffensiveImpact> GetDeffensiveýmpacts()
    {
        return DeffensiveImpacts;
    }
    public bool HasPassiveImpact(Type type)
    {
        if (PassiveImpacts == null) return false;

        foreach (Impact currentImpact in PassiveImpacts)
        {
            Type impactType = currentImpact.GetType();
            if (impactType == type)
            {
                return true;
            }
        }

        foreach (Impact currentImpact in EndTurnPassiveImpacts)
        {
            Type impactType = currentImpact.GetType();
            if (impactType == type)
            {
                return true;
            }
        }

        return false;
    }
    public T TryGetImpact<T>(Type type) where T : Impact
    {
        foreach (Impact currentImpact in PassiveImpacts)
        {
            Type impactType = currentImpact.GetType();
            if (impactType == type)
            {
                return currentImpact as T;
            }
        }

        return null;
    }
    public void AddPassiveImpact(Impact impact, GameObject description, ImpactType type)
    {
        PassiveImpacts.Add(impact);
        CombatUIManager.Instance.CreateImpactIcon(impact, description, type);
    }
    public void AddEndTurnPassiveImpact(Impact impact, GameObject description, ImpactType type)
    {
        EndTurnPassiveImpacts.Add(impact);
        CombatUIManager.Instance.CreateImpactIcon(impact, description, type);
    }
    public void AddDeffensiveImpact(DeffensiveImpact impact, GameObject description, ImpactType type)
    {
        DeffensiveImpacts.Add(impact);
        CombatUIManager.Instance.CreateImpactIcon(impact, description, type);
    }
    public void AddAttackImpact(AttackImpact impact, GameObject description, ImpactType type)
    {
        AttackImpacts.Add(impact);
        CombatUIManager.Instance.CreateImpactIcon(impact, description, type);
    }
    public void RemovePassiveImpact(Impact impact)
    {
        PassiveImpacts.Remove(impact);
        CombatUIManager.Instance.RemoveImpactIcon(impact);
    }
    public void RemoveEndTurnPassiveImpact(Impact impact)
    {
        EndTurnPassiveImpacts.Remove(impact);
        CombatUIManager.Instance.RemoveImpactIcon(impact);
    }
    public void RemoveDeffensiveImpact(DeffensiveImpact impact)
    {
        DeffensiveImpacts.Remove(impact);
        CombatUIManager.Instance.RemoveImpactIcon(impact);
    }
    public void RemoveAttackImpact(AttackImpact impact)
    {
        AttackImpacts.Remove(impact);
        CombatUIManager.Instance.RemoveImpactIcon(impact);
    }
    public void OnTurnCome()
    {
        for (int i = 0; i < PassiveImpacts.Count; i++)
        {
            Impact impact = PassiveImpacts[i];
            impact.OnTurnCome();
        }

        for (int i = 0; i < DeffensiveImpacts.Count; i++)
        {
            DeffensiveImpact impact = DeffensiveImpacts[i];
            impact.OnTurnCome();
        }

    }
    public void OnTurnEnd()
    {
        for (int i = 0; i < EndTurnPassiveImpacts.Count; i++)
        {
            Impact impact = EndTurnPassiveImpacts[i];
            impact.OnTurnCome();
        }
    }
}
