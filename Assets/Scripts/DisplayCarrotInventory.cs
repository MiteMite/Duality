using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCarrotInventory : MonoBehaviour
{
    Inventory playerInventory;
    private int m_TotalCurrency;
    public Text carrotCounterText;

    private void Start()
    {
        playerInventory = Inventory.Instance;
        m_TotalCurrency = playerInventory.GetCurrencyQte();
        carrotCounterText.text = m_TotalCurrency + " x ";
        EventManager.Instance.m_OnCurrencyCollected.AddListener(Display);
        Inventory.Instance._OnInventoryChangeEvent.AddListener(Display);
    }

    private void Display()
    {
        carrotCounterText.text = m_TotalCurrency + " x ";
    }


}
