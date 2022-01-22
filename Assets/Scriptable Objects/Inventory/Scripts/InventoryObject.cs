using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Inventory", menuName = "Inventory")]
public class InventoryObject : ScriptableObject
{
    public List<CardObject> cardList = new List<CardObject>();

    public void addCard(CardObject card)
    {
        cardList.Add(card);
    }

    public void removeCard(CardObject card)
    {
        cardList.Remove(card);
    }
}

