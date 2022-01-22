using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public DragManager dragManager;
    public Inventory playerInventory;
    public Inventory vendorInventory;
    public Deck deck;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 0, LayerMask.GetMask("Draggable"));
            if (hit.collider != null)
            {
                Card card = hit.collider.GetComponent<Card>();
                if (card != null && vendorInventory.cardInInventory(card.card) && !deck.cards.Contains(card))
                {
                    Debug.Log("This is " + card.name + " !");
                    playerInventory.addCard(card.card);
                    vendorInventory.removeCard(card.card);
                    deck.AddCard(card);
                }
            }
        }
    }
}

