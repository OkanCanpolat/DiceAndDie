using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveToTargetCurve : MonoBehaviour
{
    [SerializeField] private float reachTime;
    [SerializeField] private float collisionOffset;
    [SerializeField] private float centerOffset;
    [SerializeField] private GameObject OnCollisionEffect;
    [SerializeField] private GameObject[] deactiveObjectsAfterCollision;
    [SerializeField] private float destroyCollisionEffectAfter;
    [SerializeField] private bool useTargetPosition;
    [SerializeField] private bool useTargetPositionForCollisionEffect;
    public UnityEvent OnCollision;
    private Enemy target;
    private Vector3 targetPosition;
    private Vector3 startPosition;
    private Vector3 centerPoint;
    private Vector3 relativeStartPos;
    private Vector3 relativeEndPos;

    private void Awake()
    {
        target = CombatManager.Instance.GetEnemy();
    }
    private void Start()
    {
        CalculateTargetPosition();
        SetRelativePositions();
        MoveToTarget();
    }
    private void CalculateTargetPosition()
    {
        SkinnedMeshRenderer mesh = target.GetComponentInChildren<SkinnedMeshRenderer>();
        targetPosition = mesh.bounds.center;
    }
    private void SetRelativePositions()
    {
        centerPoint = (transform.position + targetPosition) * 0.5f;
        centerPoint -= new Vector3(0, centerOffset);
        relativeStartPos = transform.position - centerPoint;
        relativeEndPos = targetPosition - centerPoint;
    }
    private void MoveToTarget()
    {
        StartCoroutine(MoveToTargetC());
    }

    private IEnumerator MoveToTargetC()
    {
        float time = 0f;

        while (time < 1)
        {
            transform.position = GetSlerpPosition(time);
            time += Time.deltaTime / reachTime;
            yield return null;
        }

        GameObject effect = Instantiate(OnCollisionEffect, targetPosition, Quaternion.identity);
        Destroy(effect, destroyCollisionEffectAfter);
        DeactivateObjects();
        OnCollision?.Invoke();

        if (useTargetPosition)
        {
            transform.root.parent = target.transform;
        }
        if (useTargetPositionForCollisionEffect)
        {
            transform.root.parent = target.transform;
        }
    }
    private Vector3 GetSlerpPosition(float time)
    {
        return Vector3.Slerp(relativeStartPos, relativeEndPos, time) + centerPoint;
    }
    private void DeactivateObjects()
    {
        foreach (GameObject obj in deactiveObjectsAfterCollision)
        {
            if (obj != null)
            {
                ParticleSystem ps = obj.GetComponent<ParticleSystem>();
                if (ps != null) ps.Stop();
                else obj.SetActive(false);
            }
        }
    }

}
