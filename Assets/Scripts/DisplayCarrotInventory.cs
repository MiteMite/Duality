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
        playerInventory = Inventory.Instance;
        lvlCurrencyTotal = GameObject.FindGameObjectsWithTag("CurrencySpawner").Length;
    }

    private void Update()
    {
        carrotCounterText.text = playerInventory.GetTmpCurrQte() + " / " + lvlCurrencyTotal;
    }
}
