using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Direction : MonoBehaviour
{
    public MoveDirections direction;
    [SerializeField] private DirectionController controller;
    [SerializeField] private Color pointerEnterColor;
    [SerializeField] private Color defaultColor;
    private Renderer currentRenderer;

    private void Awake()
    {
        currentRenderer = GetComponent<Renderer>();
        defaultColor = currentRenderer.sharedMaterial.color;
        pointerEnterColor = Color.blue;
    }

    public void OnChose()
    {
        controller.ChoseDirection(direction);
    }
    public void OnPointerEnter()
    {
        currentRenderer.material.color = pointerEnterColor;
    }
    public void OnPointerExit()
    {
        currentRenderer.material.color = defaultColor;
    }

    private void OnMouseEnter()
    {
        OnPointerEnter();
    }
    private void OnMouseExit()
    {
        OnPointerExit();
    }
}
