using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour, IPhaseListener, IDeathListener
{
    public Inventory playerInventory;


    public void Start()
    {
        EventManager.Instance.RegisterPhaseListener(this);
        EventManager.Instance.RegisterDeathListener(this);
        playerInventory = Inventory.Instance;

    }

    void OnDestroy()
    {
        if(EventManager.Instance != null)
        {
            EventManager.Instance.UnregisterPhaseListener(this);
            EventManager.Instance.UnregisterDeathListener(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInventory.IncrementTmpCurrency();
            EventManager.Instance.SendCurrencyCollectedEvent();
            Destroy(this.gameObject);
            Debug.Log(this.gameObject.name + "collided");
        }
    }

    public void OnPhaseChangeEvent(BaseLevelStat levelStat)
    {
        if(levelStat == LevelStateManager.Instance.placementState)
        {
            Debug.Log("Currency Destroyed by Phase Change Event");
            Destroy(this.gameObject);
        }
    }

    public void OnDeathEvent()
    {
        Debug.Log("Currency Destroyed by Player Death");
        Destroy(this.gameObject);
    }
}
