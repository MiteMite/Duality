using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour
{
    Inventory playerInventory;

    public Text lvlScoreText;
    public Text totalScoreText;
    public Text currencyScoreText;

    private void Start()
    {
        playerInventory = Inventory.Instance;

    }

    private void Update()
    {
        lvlScoreText.text = "Level Score : " + playerInventory.GetCurrentLvlScore();
        totalScoreText.text = "Total Score : " + playerInventory.GetPlayerScore();
    }
}
