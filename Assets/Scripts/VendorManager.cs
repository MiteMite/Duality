using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendorManager : MonoBehaviour, IPhaseListener
{
    private FullCard[] levelVendorCards = new FullCard[3];
    private GameObject[] instVendorCards = new GameObject[3];
    public bool rewardState = false;
    public Card cardPrefab;
    public DayCard[] dayCards;
    public NightCard[] nightCards;


    private Inventory m_PlayerInventory;

    public void OnPhaseChangeEvent(BaseLevelStat levelStat)
    {
        if (levelStat == LevelStateManager.Instance.rewardState)
        {
            //spawn les cartes
            rewardState = true;
            GetComponent<SpriteRenderer>().enabled = true;

            if (levelVendorCards.Length == 3)
            {
                Card newCard = Instantiate(cardPrefab, new Vector3(0,0,0), Quaternion.identity);
                newCard.card = levelVendorCards[0];
                newCard.setSprite();
                instVendorCards[0] = newCard.gameObject;

                newCard = Instantiate(cardPrefab, new Vector3(-10, 0, 0), Quaternion.identity);
                newCard.card = levelVendorCards[1];
                newCard.setSprite();
                instVendorCards[1] = newCard.gameObject;

                newCard = Instantiate(cardPrefab, new Vector3(10, 0, 0), Quaternion.identity);
                newCard.card = levelVendorCards[2];
                newCard.setSprite();
                instVendorCards[2] = newCard.gameObject;
            }
        }
    }

    public void Start()
    {
        EventManager.Instance.RegisterPhaseListener(this);
        m_PlayerInventory = Inventory.Instance;
        transform.position = new Vector3(0, 4, 0);
        GetComponent<SpriteRenderer>().enabled = false;

        for (int i = 0; i < levelVendorCards.Length; i++)
        {
            levelVendorCards[i] = new FullCard();
            levelVendorCards[i].daySide = dayCards[Random.Range(0, dayCards.Length)];
            levelVendorCards[i].nightSide = nightCards[Random.Range(0, nightCards.Length)];
            levelVendorCards[i].isNight = Random.Range(0, 2) == 1;
        }
    }

    private void Update()
    {
        if(rewardState)
            if (Input.GetMouseButtonUp(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 0, LayerMask.GetMask("Draggable"));
                if (hit.collider != null)
                {
                    Card card = hit.collider.GetComponent<Card>();
                    if (card != null && !Deck.Instance.cards.Contains(card)
                      && (Constants.CARD_PRICE <= m_PlayerInventory.GetCurrencyQte()))
                    {
                        Debug.Log("This is the player currency : " + m_PlayerInventory.GetCurrencyQte());
                        Debug.Log("This is the price of a card : " + Constants.CARD_PRICE);
                        Debug.Log("I am saying that " + m_PlayerInventory.GetCurrencyQte() + " <= " + Constants.CARD_PRICE);
                        Debug.Log("This is " + card.name + " !");
                        Debug.Log(Constants.CARD_PRICE <= m_PlayerInventory.GetCurrencyQte());
                        Inventory.Instance.AddCard(card.card);
                        Inventory.Instance.RemoveCurrency(Constants.CARD_PRICE);
                        Deck.Instance.AddCard(card);
                        rewardState = false;
                        for (int i = 0; i < levelVendorCards.Length; i++)
                        {
                            if (card.gameObject != instVendorCards[i])
                            {
                                Destroy(instVendorCards[i]);
                            }
                        }
                        EventManager.Instance.UnregisterPhaseListener(this);
                    }
                    else
                    {
                        Debug.Log("Not enough MOOLAH");
                    }
                }
            }
    }
}
