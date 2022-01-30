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

    private int m_PlayerScore;
    private int m_LvlScore;



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

        DragManager.Instance.m_OnCardAddEvent.AddListener(CardAddEvent);
        DragManager.Instance.m_OnCardRemovedEvent.AddListener(CardRemovedEvent);

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

    public int GetTmpCurrQte()
    {
        return m_TmpCurrency;
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

            m_PlayerScore += m_LvlScore;
            m_LvlScore = 0;

            m_PlayerScore += (m_CurrencyQte * Constants.CURRENCY_VALUE);
        }

        //Debug.Log(levelStat);
        //Debug.Log("PlayerCurrency : " + m_CurrencyQte);
        //Debug.Log("TmpCurrency : " + m_TmpCurrency);
    }

    public void OnDeathEvent()
    {
        m_TmpCurrency = 0;
        Debug.Log("On Death Event of Inventory works");
    }

    public int DeckValue()
    {
        int deckValue = 0;

        foreach(FullCard card in cardList)
        {
            deckValue += card.daySide.cardValue;
            deckValue += card.nightSide.cardValue;
        }

        return deckValue;
    }

    public void CardAddEvent(int cardValue)
    {
        m_LvlScore += cardValue;
    }

    public void CardRemovedEvent(int cardValue)
    {
        m_LvlScore -= cardValue;
    }

    public int GetPlayerScore()
    {
        return m_PlayerScore;
    }

    public int GetCurrentLvlScore()
    {
        return m_LvlScore;
    }

    public int GetScoreFromCurrency()
    {
        return m_CurrencyQte * Constants.CURRENCY_VALUE;
    }
    
}
