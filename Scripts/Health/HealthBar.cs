using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health hp;
    [SerializeField] private float healthChangeSpeed = 1f;
    [SerializeField] private Image hpImage;
    [SerializeField] private TMP_Text healthText;
    private float targetHealthLevel = 1f;

    private void Awake()
    {
        hp.OnHealthChanged += ChangeHealth;
    }

    public Health GetHealth()
    {
        return hp;
    }
    public void SetHealth(Health health)
    {
        hp = health;
    }
    private void ChangeHealth(float currentHelath, float maxHealth)
    {
        StartCoroutine(ChangeHealthC(currentHelath, maxHealth));
    }
    private IEnumerator ChangeHealthC(float currentHealth, float maxHealth)
    {
        targetHealthLevel = currentHealth / maxHealth;
        healthText.text = currentHealth.ToString();

        float elapsedTime = 0f;

        while (elapsedTime < healthChangeSpeed)
        {
            hpImage.fillAmount =
                Mathf.MoveTowards(hpImage.fillAmount, targetHealthLevel, healthChangeSpeed * Time.deltaTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        hpImage.fillAmount = targetHealthLevel;
    }
}
