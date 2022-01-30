using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScoreDisplay : MonoBehaviour
{
    Inventory playerInventory;
    private int m_TotalScore;
    private int m_BonusScore;
    private int m_CarrotQte;
    private int m_FinalScore;
    public Text finalScoreCurrency;

    // Start is called before the first frame update
    void Start()
    {
        playerInventory = Inventory.Instance;
        m_TotalScore = playerInventory.GetPlayerScore();
        m_CarrotQte = playerInventory.GetCurrencyQte();
        m_BonusScore = m_CarrotQte * Constants.CURRENCY_VALUE;
        m_FinalScore = m_TotalScore + m_BonusScore;

        finalScoreCurrency.text = "Total score : " + m_TotalScore + "\n" + 
            "Bonus Score : " + Constants.CURRENCY_VALUE + " x " 
            + m_CarrotQte + " = " + m_BonusScore + "\n" + 
            "Final Score : " + m_TotalScore + " + " + m_BonusScore + " = " + m_FinalScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
