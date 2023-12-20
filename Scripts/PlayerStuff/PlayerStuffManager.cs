using System;
using UnityEngine;

public class PlayerStuffManager : MonoBehaviour
{
    public event Action<int> OnFoodChanged;
    public event Action<int> OnKeyChanged;
    [SerializeField] private float foodHealAmount;
    [SerializeField] private int keyAmount;
    private int foodAmount = 2;
    private PlayerHealth health;

    private void Awake()
    {
        health = GetComponent<PlayerHealth>();
    }

    private void Start()
    {
        OnFoodChanged?.Invoke(foodAmount);
        OnKeyChanged?.Invoke(keyAmount);
    }

    public int GetFood()
    {
        return foodAmount;
    }
    public void IncreaseFood(int amount)
    {
        foodAmount += amount;
        OnFoodChanged?.Invoke(foodAmount);
    }
    public void UseFood()
    {
        if (foodAmount <= 0 || !DiceController.Instace.DiceEnable) return;

        foodAmount--;
        OnFoodChanged?.Invoke(foodAmount);
        health.RestoreHealth(foodHealAmount);
    }
    public int GetKey()
    {
        return keyAmount;
    }
    public void IncreaseKey(int amount)
    {
        keyAmount += amount;
        keyAmount = Mathf.Clamp(keyAmount, 0, 400);
        OnKeyChanged?.Invoke(keyAmount);
    }
}
