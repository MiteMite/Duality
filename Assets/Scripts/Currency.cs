using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour
{
    public Inventory playerInventory;

    public void Start()
    {
        playerInventory = Inventory.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInventory.AddCurrency();
            Destroy(this.gameObject);
        }
    }
}
