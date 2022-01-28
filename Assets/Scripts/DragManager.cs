using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragManager : MonoBehaviour
{
    private static DragManager _instance;
    private Vector3 offset;
    [HideInInspector]
    public Card currentCard = null;
    private Deck deck;
    public static DragManager Instance { get => _instance; set => _instance = value; }

    public void Awake()
    {
        if (_instance == null)
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
        if(!deck)
            deck = GetComponent<Deck>();
    }
    private void Update()
    {
        if (LevelStateManager.Instance.m_currentState == LevelStateManager.Instance.placementState)
        {
            if (Input.GetMouseButtonDown(0))//on pickup
            {
                Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10));
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 0, LayerMask.GetMask("Draggable"));
                if (hit.collider != null)
                {
                    Card card = hit.collider.GetComponent<Card>();
                    if (card != null)
                    {
                        currentCard = card;
                        currentCard.GetComponent<SpriteRenderer>().sortingOrder = 100;
                        offset = card.transform.position - mousepos;
                        deck.RemoveCard(card);
                    }
                }

            }else if (Input.GetMouseButton(0))//on drag
            {
                if(currentCard != null)
                {
                    Vector3 cardPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
                    cardPos.z = currentCard.transform.position.z;
                    currentCard.transform.position = cardPos;
                }
            }
            else if (Input.GetMouseButtonUp(0))//on release
            {
                if (!currentCard) return;
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 0, LayerMask.GetMask("Droppable"));
                if (hit.collider != null)
                {
                    if (hit.collider.GetComponent<Deck>())
                    {
                        deck.AddCard(currentCard);
                    }
                    else
                    {
                        currentCard.MoveTo(hit.collider.transform.position);
                        currentCard.draggable = hit.collider.gameObject;
                    }
                }
                else
                {
                    deck.AddCard(currentCard);
                }
                currentCard = null;
            }

        }
    }
}
