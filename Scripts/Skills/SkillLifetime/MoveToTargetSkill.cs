using System;
using System.Collections;
using UnityEngine;

public class MoveToTargetSkill : MonoBehaviour
{
    private Vector3 target;
    private float speed;
    private float reachOffset;
    private float delay;
    public event Action OnReach;
    public void Initizlize(Vector3 enemy, float speed, float offset, float delay)
    {
        target = enemy;
        this.speed = speed;
        this.delay = delay;
        reachOffset = offset;
    }
    public void Move()
    {
        StartCoroutine(MoveToTarget());
    }
    private IEnumerator MoveToTarget()
    {
        yield return new WaitForSeconds(delay);
        transform.parent = null;

        while (Vector3.Distance(target, transform.position) > reachOffset)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
            yield return null;
        }

        OnReach?.Invoke();
    }
}
