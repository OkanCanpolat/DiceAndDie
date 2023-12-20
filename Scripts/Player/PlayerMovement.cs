using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private MovePointBase currentPoint;
    [SerializeField] private DiceController dice;
    [SerializeField] private DirectionController directionController;
    [SerializeField] private float distanceThreshold;
    [SerializeField] private float rotationSpeed;
    private Animator playerAnimator;
    private NavMeshAgent agent;
    private int remainingStepCount;
    private int speedParameterID;
    private MoveDirections selectedDirection;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        playerAnimator = GetComponent<Animator>();
        dice.OnDiceRoll += SetStepCount;
    }

    private void Start()
    {
        speedParameterID = Animator.StringToHash("Speed");
    }
    private void SetStepCount(int value)
    {
        remainingStepCount = value;
        Invoke("ControlMovement", 1f);
    }
    public void ControlMovement()
    {
        if (remainingStepCount > 0)
        {
            ControlNextStep();
        }

        else
        {
            ApplyCurrentPoint();
        }
    }
    private IEnumerator MoveToTarget(MovePointBase targetPoint)
    {
        agent.SetDestination(targetPoint.transform.position);
        playerAnimator.SetFloat(speedParameterID, 1);

        while (Vector3.Distance(transform.position, agent.destination) > distanceThreshold)
        {
            yield return null;
        }

        currentPoint = targetPoint;
        remainingStepCount--;
        ControlMovement();
    }
    public void SetDirectionAndMove(MoveDirections direction)
    {
        selectedDirection = direction;
        MovePointBase target = currentPoint.GetMovePoint(selectedDirection);
        StartCoroutine(MoveToTarget(target));
    }
    public void RotateSmoothly(Transform target)
    {
        StartCoroutine(RotateSmoothCoroutine(target));
    }
    private IEnumerator RotateSmoothCoroutine(Transform target)
    {
        Vector3 relativePos = new Vector3(target.position.x, transform.position.y, target.position.z);
        Quaternion lookRotation = Quaternion.LookRotation(relativePos - transform.position);

        float time = 0;

        while(time < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);
            time += Time.deltaTime * rotationSpeed;
            yield return null;
        }
    }
    private void ControlNextStep()
    {
        if (currentPoint.LinkedPoints.Length > 1)
        {
            playerAnimator.SetFloat(speedParameterID, 0);
            directionController.ActivateValidDirections(currentPoint.LinkedPoints);
        }

        else if (currentPoint.LinkedPoints.Length == 0)
        {
            ApplyCurrentPoint();
        }

        else
        {
            StartCoroutine(MoveToTarget(currentPoint.LinkedPoints[0].LinkedPoint));
        }
    }
    private void ApplyCurrentPoint()
    {
        playerAnimator.SetFloat(speedParameterID, 0);
        currentPoint.Apply();
    }
}
