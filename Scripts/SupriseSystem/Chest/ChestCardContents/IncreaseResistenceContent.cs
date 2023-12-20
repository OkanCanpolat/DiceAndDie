using UnityEngine;

[CreateAssetMenu(fileName = "IncreaseResistenceContent", menuName = "SupriseContents/Chest/Increase Resistence")]
public class IncreaseResistenceContent : SupriseCardContent
{
    [SerializeField] private ResistenceType Type;
    [SerializeField] private float incrementAmount;
    public override void Action()
    {
        IncreaseResistence();
    }
    public void IncreaseResistence()
    {
        Resistences resistences = CombatManager.Instance.GetPlayer().GetComponent<Resistences>();
        switch (Type)
        {
            case ResistenceType.Ice:
                resistences.IncreaseIceResistence(incrementAmount);
                break;
            case ResistenceType.Fire:
                resistences.IncreaseFireResistence(incrementAmount);
                break;
            case ResistenceType.Electric:
                resistences.IncreaseElectricResistence(incrementAmount);
                break;
            case ResistenceType.Bleed:
                resistences.IncreaseBleedResistence(incrementAmount);
                break;
            case ResistenceType.Physical:
                resistences.IncreasePhysicalResistence(incrementAmount);
                break;
            case ResistenceType.Water:
                resistences.IncreaseWaterResistence(incrementAmount);
                break;
            case ResistenceType.Poison:
                resistences.IncreasePoisonResistence(incrementAmount);
                break;
            case ResistenceType.Dodge:
                resistences.SetDodgeChange(incrementAmount);
                break;
        }
    }
}


