using UnityEngine;

public class WaypointViewManager : MonoBehaviour
{
    [SerializeField] private GameObject waypointsParentObject;

    private void Awake()
    {
        CombatManager.Instance.OnCombatEntry += DisableWaypoints;
        CombatManager.Instance.OnCombatEnd += EnableWaypoints;
    }
    private void DisableWaypoints()
    {
        waypointsParentObject.SetActive(false);
    }
    private void EnableWaypoints()
    {
        waypointsParentObject.SetActive(true);
    }
}
