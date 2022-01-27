using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FullCard
{
    public DayCard daySide;
    public NightCard nightSide;
    public bool isNight;
}
public class Inventory : MonoBehaviour, IPhaseListener, IDeathListener
{
    private static Inventory _instance;

    public FullCard[] startingCards;
    public Card cardPrefab;

    private List<FullCard> cardList = new List<FullCard>();
    private int m_CurrencyQte;
    private int m_TmpCurrency = 0;

    public static Inventory Instance { get => _instance; set => _instance = value; }

    public void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }


    public void Start()
    {
        //add all starting cards in inventory
        for (int i = 0; i < startingCards.Length; i++)
        {
            AddCard(startingCards[i]);
            Card newCard = Instantiate(cardPrefab, transform);
            newCard.card = startingCards[i];
            GetComponent<Deck>().AddCard(newCard);
        }

        EventManager.Instance.RegisterPhaseListener(this);

    }

    void OnDestroy()
    {
        if(EventManager.Instance != null)
        {
            EventManager.Instance.UnregisterPhaseListener(this);
        }
    }

    public void AddCard(FullCard card)
    {
        cardList.Add(card);
        // Debug.Log("Card added to " + this.name);
    }

    public void AddCurrency()
    {
        m_CurrencyQte++;
        //Debug.Log("I am now holding " + m_CurrencyQte + " buckarinos !");
    }

    public void RemoveCard(FullCard card)
    {
        cardList.Remove(card);
        //Debug.Log("Card removed from " + this.name);
    }

    public bool RemoveCurrency(int price)
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

    public int GetCurrencyQte()
    {
        return m_CurrencyQte;
    }

    public bool CardInInventory(FullCard card)
    {
        return cardList.Contains(card);
    }

    public void IncrementTmpCurrency()
    {
        m_TmpCurrency++;
    }

    public void EmptyTmpCurrency()
    {
        m_TmpCurrency = 0;
    }

    public void OnPhaseChangeEvent(BaseLevelStat levelStat)
    {
        if(levelStat == LevelStateManager.Instance.placementState)
        {
            m_TmpCurrency = 0;
        }
        else if(levelStat == LevelStateManager.Instance.rewardState)
        {
            m_CurrencyQte += m_TmpCurrency;
            m_TmpCurrency = 0;
        }

        Debug.Log(levelStat);
        Debug.Log("PlayerCurrency : " + m_CurrencyQte);
        Debug.Log("TmpCurrency : " + m_TmpCurrency);
    }

    public void OnDeathEvent()
    {
        m_TmpCurrency = 0;
    }
}
