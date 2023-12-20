using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class DirectionLightController : MonoBehaviour
{
    public static DirectionLightController Instance;
    [SerializeField] private Light directionLight;
    private Color defaultLightColor;
    private void Awake()
    {
        #region Singleton
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion
        defaultLightColor = directionLight.color;
    }
    public float GetIntensity()
    {
        return directionLight.intensity;
    }

    public void ChangeLightIntensity(float time, float amount)
    {
        StartCoroutine(ChangeLight(time, amount));
    }
    private IEnumerator ChangeLight(float time, float amount)
    {
        float t = 0;
        float start = directionLight.intensity;
        float destination = directionLight.intensity + amount;

        while (t < 1)
        {
            directionLight.intensity = Mathf.Lerp(start, destination, t);
            t += Time.deltaTime / time;
            yield return null;
        }

        directionLight.intensity = destination;
    }

    public void ChangeLightColor(Color color)
    {
        directionLight.color = color;
    }
    public void ResetLightColor()
    {
        directionLight.color = defaultLightColor;
    }
}
