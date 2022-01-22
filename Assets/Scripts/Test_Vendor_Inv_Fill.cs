using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Vendor_Inv_Fill : MonoBehaviour
{
    public List<Card> cards = new List<Card>();
    public Inventory vendorInventory;

    void Start()
    {
        foreach(Card c in cards)
        {
            vendorInventory.addCard(c.card);
        }
    }
}
