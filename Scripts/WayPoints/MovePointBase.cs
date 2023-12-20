using UnityEngine;

public abstract class MovePointBase : MonoBehaviour
{
    public  LinkedMovePoint[] LinkedPoints;
    public abstract void Apply();
    public virtual MovePointBase GetMovePoint(MoveDirections direction)
    {
        foreach(LinkedMovePoint linkedPoint in LinkedPoints)
        {
            if(linkedPoint.Direction == direction)
            {
                return linkedPoint.LinkedPoint;
            }
        }
        return null;
    }
}
