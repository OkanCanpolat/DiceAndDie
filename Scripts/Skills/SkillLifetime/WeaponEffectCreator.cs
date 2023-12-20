using System.Collections.Generic;
using UnityEngine;

public class WeaponEffectCreator : MonoBehaviour
{
    public GameObject Mesh;
    private List<ParticleSystem> particles = new List<ParticleSystem>();

    private void LateUpdate()
    {
        if (Mesh == null) return;
        UpdateEffects();
    }
    public void Initialize()
    {
        ParticleSystem[] allParticles = GetComponentsInChildren<ParticleSystem>();
        float particleScale = Mesh.GetComponent<Renderer>().bounds.size.magnitude;
        MeshRenderer meshRenderer = Mesh.GetComponent<MeshRenderer>();

        foreach (ParticleSystem ps in allParticles)
        {
            ps.gameObject.SetActive(false);
            var main = ps.main;
            main.startSizeMultiplier /= particleScale;

            var shape = ps.shape;
            shape.useMeshColors = false;
            shape.meshShapeType = ParticleSystemMeshShapeType.Triangle;
            shape.shapeType = ParticleSystemShapeType.MeshRenderer;
            shape.meshRenderer = meshRenderer;
            ps.gameObject.SetActive(true);

            particles.Add(ps);
        }
    }
    private void UpdateEffects()
    {
        Transform meshT = Mesh.transform;

        foreach (ParticleSystem particle in particles)
        {
            Transform parent = particle.transform.parent;
            parent.position = meshT.position;
            parent.rotation = meshT.rotation;
            parent.localScale = meshT.lossyScale;
        }
    }
}
