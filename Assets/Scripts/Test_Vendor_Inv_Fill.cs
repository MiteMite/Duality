using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Vendor_Inv_Fill : MonoBehaviour
{
    public List<FullCard> cards = new List<FullCard>();
    public Inventory vendorInventory;

    void Start()
    {
        foreach(FullCard c in cards)
        {
            vendorInventory.AddCard(c);
        }
    }
}
