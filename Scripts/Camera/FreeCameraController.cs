using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCameraController : MonoBehaviour
{
    [SerializeField] private DiceController diceController;
    [SerializeField] private float cameraSpeed;
    private CinemachineVirtualCamera virtualCamera;
    private CinemachineTransposer transposer;
    private CinemachineConfiner confiner;
    private Vector3 startingOffset;
    private float horizontal;
    private float vertical;
    private const string inputHorizontalName = "Horizontal";
    private const string inputVerticallName = "Vertical";

    private Vector3 maxOffset;
    private Vector3 minOffset;

    private float xOffset;
    private float zOffset;
    private float yOffset;

    private void Awake()
    {
        diceController.OnFreeViewActivated += ResetCameraOffset;
        diceController.OnFreeViewActivated += CalculateMaxMinOffset;

        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        startingOffset = transposer.m_FollowOffset;

        confiner = GetComponent<CinemachineConfiner>();
    }

    private void Update()
    {
        if (!diceController.CanRoll)
        {
            horizontal = Input.GetAxis(inputHorizontalName);
            vertical = Input.GetAxis(inputVerticallName);

            xOffset += horizontal;
            zOffset += vertical;
            yOffset = transposer.m_FollowOffset.y;

            zOffset = Mathf.Clamp(zOffset, minOffset.x, maxOffset.x);
            xOffset = Mathf.Clamp(xOffset, -maxOffset.z, -minOffset.z);

            transposer.m_FollowOffset = new Vector3(zOffset, yOffset, -xOffset);
        }
    }
    private void ResetCameraOffset()
    {
        xOffset = 0;
        zOffset = 0;
        transposer.m_FollowOffset = startingOffset;
    }
    private void CalculateMaxMinOffset()
    {
        Transform playerTransform = virtualCamera.Follow;
        Collider confinerVolume = confiner.m_BoundingVolume;

        Vector3 max = confinerVolume.bounds.max;
        Vector3 min = confinerVolume.bounds.min;

        maxOffset = max - playerTransform.transform.position;
        minOffset = min - playerTransform.transform.position;
    }
}
