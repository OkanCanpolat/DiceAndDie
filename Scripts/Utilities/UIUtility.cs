using UnityEngine;

public static class UIUtility 
{
    public static Vector2 GenerateHorizontalPosition(RectTransform element, float padding, Canvas root)
    {
        float rectWidth = element.rect.width;
        int screenWidth = Screen.width;

        Vector2 currentPosition = element.anchoredPosition;
        Vector2 pixelCoordinates = RectTransformUtility.PixelAdjustPoint(element.transform.position, element, root);
        float leftSpace = pixelCoordinates.x;
        float leftExcess = leftSpace - rectWidth / 2;

        float rightSpace = screenWidth - leftSpace;
        float rightExcess = rightSpace - rectWidth / 2;

        if (leftExcess < padding)
        {
            currentPosition.x += padding + (-leftExcess);
            return currentPosition;
        }

        if (rightExcess < padding)
        {
            currentPosition.x -= padding + (-rightExcess);
            return currentPosition;
        }

        return currentPosition;
    }
}
