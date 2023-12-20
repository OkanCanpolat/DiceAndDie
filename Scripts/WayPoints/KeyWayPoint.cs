using UnityEngine;
using System.Collections;

public class KeyWayPoint : MovePointBase
{
    [SerializeField] private PlayerStuffManager stuffManager;
    [SerializeField] private AudioClip clip;
    private int keyAmount = 50;
    private AudioSource audioSource;
    private float keyDelay = 0.5f;
    private float diceDelay = 1;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public override void Apply()
    {
        audioSource.PlayOneShot(clip);
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(keyDelay);
        stuffManager.IncreaseKey(keyAmount);
        yield return new WaitForSeconds(diceDelay);
        DiceController.Instace.OpenDice();
    }
}
