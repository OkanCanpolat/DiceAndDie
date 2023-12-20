using System.Collections;
using UnityEngine;

public class EmptyMovePoint : MovePointBase
{
    [SerializeField] private float openDiceTime = 1f;
    
    public override void Apply()
    {
        StartCoroutine(Dice());
    }
    private IEnumerator Dice()
    {
        yield return new WaitForSeconds(openDiceTime);
        DiceController.Instace.OpenDice();
    }
}
