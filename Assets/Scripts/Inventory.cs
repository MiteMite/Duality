using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    private List<CardObject> cardList = new List<CardObject>();
    private int m_CurrencyQte;

    public void addCard(CardObject card)
    {
        cardList.Add(card);
        Debug.Log("Card added to " + this.name);
    }

    public void AddCurrency()
    {
        m_CurrencyQte++;
        Debug.Log("I am now holding " + m_CurrencyQte + " buckarinos !");
    }

    public void removeCard(CardObject card)
    {
        cardList.Remove(card);
        Debug.Log("Card removed from " + this.name);
    }

    public bool removeCurrency(int price)
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

    public bool cardInInventory(CardObject card)
    {
        return cardList.Contains(card);
    }

}
