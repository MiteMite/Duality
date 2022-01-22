using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragManager : MonoBehaviour
{
    public bool dragging = false;
    public Vector3 offset;
    public Card currentCard = null;
    public Deck deck;

    private void Update()
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
