using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCarrotInventory : MonoBehaviour
{
    Inventory playerInventory;
    private int lvlCurrencyTotal;
    public Text carrotCounterText;

    private void Start()
    {
//        LevelStateManager.Instance.m_OnPhaseChangeEvent
        playerInventory = Inventory.Instance;
        lvlCurrencyTotal = GameObject.FindGameObjectsWithTag("CurrencySpawner").Length;
        carrotCounterText.text = playerInventory.GetTmpCurrQte() + " / " + lvlCurrencyTotal;
        EventManager.Instance.m_OnCurrencyCollected.AddListener(Display);
        Inventory.Instance._OnInventoryChangeEvent.AddListener(Display);
    }

    private void Display()
    {
        carrotCounterText.text = playerInventory.GetTmpCurrQte() + " / " + lvlCurrencyTotal;
    }


}
