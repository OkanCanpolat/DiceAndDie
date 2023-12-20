using UnityEngine;

public class CharacterViewChanger : MonoBehaviour
{
    [SerializeField] private Material[] originalMaterials;
    [SerializeField] private GameObject hairParent;
    [SerializeField] private GameObject clothesParent;
    [SerializeField] private SkinnedMeshRenderer characterRenderer;

    private void Awake()
    {
        originalMaterials = characterRenderer.sharedMaterials;
    }
    public void DeactivateClothesAndHair()
    {
        clothesParent.SetActive(false);
        hairParent.SetActive(false);
    }
    public void ActivateClothesAndHair()
    {
        clothesParent.SetActive(true);
        hairParent.SetActive(true);
    }
    public void ChangeView(Material[] materials)
    {
        Material[] rendererMaterials = characterRenderer.materials;
        rendererMaterials = materials;
        characterRenderer.materials = rendererMaterials;
    }
    public void ChangeViewToOriginal()
    {
        Material[] rendererMaterials = characterRenderer.materials;
        rendererMaterials = originalMaterials;
        characterRenderer.materials = rendererMaterials;
    }
}
