using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionController : MonoBehaviour
{
    public Direction[] Directions;
    [SerializeField] private PlayerMovement playerMovement;
    public void ActivateValidDirections(LinkedMovePoint[] points)
    {
        foreach(LinkedMovePoint point in points)
        {
            Direction direction = GetDirection(point.Direction);
            direction.gameObject.SetActive(true);
        }
    }
    public void ChoseDirection(MoveDirections direction)
    {
        playerMovement.SetDirectionAndMove(direction);
        CloseDirections();
    }
    public void CloseDirections()
    {
        foreach (Direction direction in Directions)
        {
            direction.gameObject.SetActive(false);
        }
    }
    public Direction GetDirection(MoveDirections way)
    {
        foreach(Direction direction in Directions)
        {
            if(direction.direction == way)
            {
                return direction;
            }
        }

        return null;
    }
}
