using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveToTargetArrow : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float collisionOffset;
    [SerializeField] private GameObject OnCollisionEffect;
    [SerializeField] private GameObject[] deactiveObjectsAfterCollision;
    [SerializeField] private float destroyCollisionEffectAfter;
    [SerializeField] private bool useTargetPosition;
    [SerializeField] private bool useTargetPositionForCollisionEffect;
    public UnityEvent OnCollision;
    private Enemy target;
    private Vector3 targetPosition;
    private void Start()
    {
        target = CombatManager.Instance.GetEnemy();
        CalculateTargetPosition();
        MoveToTarget();
    }
    private void CalculateTargetPosition()
    {
        SkinnedMeshRenderer mesh = target.GetComponentInChildren<SkinnedMeshRenderer>();
        targetPosition = mesh.bounds.center;
    }
    private void MoveToTarget()
    {
        StartCoroutine(MoveToTargetC());
    }
    private IEnumerator MoveToTargetC()
    {
        while(Vector3.Distance(transform.position, targetPosition) > collisionOffset)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);
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
