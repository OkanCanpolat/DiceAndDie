using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera fightCamera;
    [SerializeField] private CinemachineFreeLook diceCamera;
    [SerializeField] private CinemachineVirtualCamera freeCamera;
    [SerializeField] private CombatManager combatManager;
    private int lowerPriority = 10;
    private int highPriority = 20;


    private void Awake()
    {
        combatManager.OnCombatEntry += ChangeToCombatCamera;
        combatManager.OnCombatEnding += ChangeToDiceCamera;
    }
    public void ChangeToCombatCamera()
    {
        diceCamera.Priority = lowerPriority;
        fightCamera.Priority = highPriority;
        freeCamera.Priority = lowerPriority;

    }
    public void ChangeToDiceCamera()
    {
        diceCamera.Priority = highPriority;
        fightCamera.Priority = lowerPriority;
        freeCamera.Priority = lowerPriority;
    }
    public void ChangeToFreeCamera()
    {
        diceCamera.Priority = lowerPriority;
        fightCamera.Priority = lowerPriority;
        freeCamera.Priority = highPriority;
    }
}
