using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfHealthPoint : MovePointBase
{
    [SerializeField] private GameObject healPrefab;
    [SerializeField] private PlayerHealth target;
    private float healtAmount = 30f;
    public override void Apply()
    {
        GameObject explosion = Instantiate(healPrefab, target.transform.position + Vector3.up, Quaternion.identity);
        OnDestroyEffect effect = explosion.GetComponent<OnDestroyEffect>();
        target.RestoreHealth(healtAmount);
        effect.OnDestroyed += AfterHealth;
    }

    private void AfterHealth()
    {
        DiceController.Instace.OpenDice();
    }

}
