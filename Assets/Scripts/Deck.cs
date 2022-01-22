using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<Card> cards;
    public DragManager dragManager;
    public float width = 16;
    public int currentCardPos = 0;

    public void Start()
    {
        PlaceCards();
    }

    public void Update()
    {
        PlaceCards();
    }

    public void AddCard(Card card)
    {
        if (!cards.Contains(card))
        {
            if(cards.Count > 0)
            {
                cards.Insert(currentCardPos, card);
            }
            else
            {
                cards.Add(card);
            }
        }
        card.MoveTo(transform.position);
    }
    public void RemoveCard(Card card)
    {
        if(cards.Contains(card))
            cards.Remove(card);
    }

    private void PlaceCards()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].GetComponent<SpriteRenderer>().sortingOrder = i * 2;
            cards[i].square.GetComponent<SpriteRenderer>().sortingOrder = i * 2 + 1;
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
        if(dragManager.currentCard != null && Mathf.Abs(dragManager.currentCard.transform.position.y-transform.position.y) <3)
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
