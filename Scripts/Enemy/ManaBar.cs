using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private float manaChangeSpeed = 1f;
    [SerializeField] private Image manaImage;
    [SerializeField] private TMP_Text manaText;
    private float targetHealthLevel = 1f;

    private void Awake()
    {
        enemy.OnManaChanged += ChangeHealth;
    }

    public void SetEnemy(Enemy enemy)
    {
        this.enemy = enemy;
    }
    private void ChangeHealth(int currentHelath, int maxHealth)
    {
        StartCoroutine(ChangeHealthC(currentHelath, maxHealth));
    }
    private IEnumerator ChangeHealthC(int currentHealth, int maxHealth)
    {
        targetHealthLevel = (float)currentHealth / maxHealth;
        manaText.text = currentHealth.ToString();

        float elapsedTime = 0f;

        while (elapsedTime < manaChangeSpeed)
        {
            manaImage.fillAmount =
                Mathf.MoveTowards(manaImage.fillAmount, targetHealthLevel, manaChangeSpeed * Time.deltaTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        manaImage.fillAmount = targetHealthLevel;
    }
}
