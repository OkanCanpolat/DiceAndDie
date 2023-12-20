using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private DiceController dice;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && dice.DiceEnable)
        {
            dice.RollDice();
        }
        if (Input.GetKeyDown(KeyCode.R) && dice.CanChangeView)
        {
            dice.ChangeView();
        }
    }
}
