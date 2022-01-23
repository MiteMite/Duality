using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FullCard
{
    public DayCard daySide;
    public NightCard nightSide;
    public bool isNight;
}
public class Inventory : MonoBehaviour
{
    public static Inventory playerInventory

    public FullCard[] startingCards;
    public Card cardPrefab;

    private List<FullCard> cardList = new List<FullCard>();
    private int m_CurrencyQte;

    public void Start()
    {
        //add all starting cards in inventory
        for (int i = 0; i < startingCards.Length; i++)
        {
            AddCard(startingCards[i]);
            Card newCard = Instantiate(cardPrefab, transform);
            newCard.card = startingCards[i];
            GetComponent<Deck>().AddCard(newCard);
        }
    }

    public void AddCard(FullCard card)
    {
        cardList.Add(card);
        // Debug.Log("Card added to " + this.name);
    }

    public void AddCurrency()
    {
        m_CurrencyQte++;
        //Debug.Log("I am now holding " + m_CurrencyQte + " buckarinos !");
    }

    public void RemoveCard(FullCard card)
    {
        cardList.Remove(card);
        //Debug.Log("Card removed from " + this.name);
    }

    public bool RemoveCurrency(int price)
    {
        if(m_CurrencyQte < price)
        {
            return false;
        }
        else
        {
            m_CurrencyQte -= price;
            return true;
        }
    }

    public bool CardInInventory(FullCard card)
    {
        return cardList.Contains(card);
    }

}
