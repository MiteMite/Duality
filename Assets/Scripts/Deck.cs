using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    private static Deck _instance;
    //[HideInInspector]
    public List<Card> cards;
    private DragManager dragManager;
    private float width = 16;
    private int currentCardPos = 0;

    public static Deck Instance { get => _instance; set => _instance = value; }

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
        if(!dragManager)
            dragManager = GetComponent<DragManager>();
    }

    public void Update()
    {
        PlaceCards();
    }

    public void AddCard(Card card)
    {
        if (!cards.Contains(card))
        {
            if(cards.Count > 0 && currentCardPos >= 0 && currentCardPos < cards.Count)
            {
                cards.Insert(currentCardPos, card);
            }
            else
            {
                cards.Add(card);
            }
            card.transform.parent = transform;
            card.draggable = null;
            card.MoveTo(transform.position);
        }
    }
    public void RemoveCard(Card card)
    {
        if(cards.Contains(card))
            cards.Remove(card);
    }

    private void PlaceCards()
    {
        currentCardPos = 0;
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].GetComponent<SpriteRenderer>().sortingOrder = i;
            Vector3 target = GetPosition(i);
            if(cards[i].target != target)
                cards[i].MoveTo(target);
        }
    }

    private Vector3 GetPosition(int i)
    {
        width = cards.Count;
        int numCards = cards.Count-1;
        if (numCards == 0) numCards = 1;
        Vector3 target = transform.position;
        float targetx = transform.position.x - (width / 2) + (width / numCards) * i;
        if (dragManager.currentCard != null && Mathf.Abs(dragManager.currentCard.transform.position.y-transform.position.y) <3)
        {
            //move autour de la currentCard
            float currentCardWidth = 3f;
            //width += currentCardWidth;
            targetx = transform.position.x - (width / 2) + (width / (numCards+2)) * i;
            if (targetx > (dragManager.currentCard.transform.position.x - currentCardWidth/2))
            {
                targetx += currentCardWidth;
            }
            else
            {
                currentCardPos = i+1;
            }
        }
        else
        {
            targetx = transform.position.x - (width / 2) + (width / numCards) * i;
            currentCardPos = cards.Count;
        }
        target.x = targetx;

        return target;
    }

}
