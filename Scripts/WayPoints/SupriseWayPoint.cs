using System.Collections.Generic;
using UnityEngine;

public class SupriseWayPoint : MovePointBase
{
    [SerializeField] private GameObject supriseCanvas;
    [SerializeField] private SupriseCardContent NegativeContent;
    [SerializeField] private List<SupriseCardContent> PositiveContents;
    [SerializeField] private List<SupriseCard> Cards;

    public override void Apply()
    {
        SetCardContents();
        ActivateButtons();
        supriseCanvas.SetActive(true);
    }

    public void DeactivateButtons()
    {
        foreach (SupriseCard card in Cards)
        {
            card.Button.enabled = false;
        }
    }
    public void ActivateButtons()
    {
        foreach (SupriseCard card in Cards)
        {
            card.Button.enabled = true;
        }
    }
    public void FinishAction()
    {
        supriseCanvas.SetActive(false);
        DiceController.Instace.OpenDice();
    }
    private void SetCardContents()
    {
        List<SupriseCard> tempCards = new List<SupriseCard>();

        for (int i = 0; i < Cards.Count; i++)
        {
            tempCards.Add(Cards[i]);
        }

        int negativeCardNumber = Random.Range(0, tempCards.Count);
        tempCards[negativeCardNumber].Init(NegativeContent, this);
        tempCards.RemoveAt(negativeCardNumber);

        for (int i = 0; i < tempCards.Count;)
        {
            int randomContentIndex = Random.Range(0, PositiveContents.Count);
            SupriseCardContent content = PositiveContents[randomContentIndex];
            int cardNumber = Random.Range(0, tempCards.Count);
            tempCards[cardNumber].Init(content, this);
            tempCards.RemoveAt(cardNumber);
            PositiveContents.Remove(content);
        }
    }
}
