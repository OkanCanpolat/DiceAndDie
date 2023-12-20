using UnityEngine;

public class Resistences : MonoBehaviour
{
    [SerializeField] private float fireResistence;
    [SerializeField] private float iceResistence;
    [SerializeField] private float physicalResistence;
    [SerializeField] private float poisonResistence;
    [SerializeField] private float electricResistence;
    [SerializeField] private float waterResistence;
    [SerializeField] private float bleedResistence;
    [SerializeField] private float dodgeChance;
    public float GetFireResistence()
    {
        return fireResistence;
    }
    public void IncreaseFireResistence(float value)
    {
        fireResistence += value;
    }
    public float GetIceResistence()
    {
        return iceResistence;
    }
    public void IncreaseIceResistence(float value)
    {
        iceResistence += value;
    }
    public float GetPhysicalResistence()
    {
        return physicalResistence;
    }
    public void IncreasePhysicalResistence(float value)
    {
        physicalResistence += value;
    }
    public float GetPoisonResistence()
    {
        return poisonResistence;
    }
    public void IncreasePoisonResistence(float value)
    {
        poisonResistence += value;
    }
    public float GetElectricResistence()
    {
        return electricResistence;
    }
    public void IncreaseElectricResistence(float value)
    {
        electricResistence += value;
    }
    public float GetWaterResistence()
    {
        return waterResistence;
    }
    public void IncreaseWaterResistence(float value)
    {
        waterResistence += value;
    }
    public float GetDodgeChange()
    {
        return dodgeChance;
    }
    public void SetDodgeChange(float value)
    {
        dodgeChance += value;
    }
    public float GetBleedResistence()
    {
        return bleedResistence;
    }
    public void IncreaseBleedResistence(float value)
    {
        bleedResistence += value;
    }
}
