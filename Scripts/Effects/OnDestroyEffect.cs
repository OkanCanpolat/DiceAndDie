using System;
using UnityEngine;

public class OnDestroyEffect : MonoBehaviour
{
    public Action OnDestroyed;

    private void OnDestroy()
    {
        OnDestroyed?.Invoke();
    }
}
