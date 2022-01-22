using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    private List<CardObject> cardList = new List<CardObject>();

    public void addCard(CardObject card)
    {
        cardList.Add(card);
        Debug.Log("Card added to " + this.name);
    }

    public void removeCard(CardObject card)
    {
        cardList.Remove(card);
        Debug.Log("Card removed from " + this.name);
    }

    public bool cardInInventory(CardObject card)
    {
        return cardList.Contains(card);
    }

}
