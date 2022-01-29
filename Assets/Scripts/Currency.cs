using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour, IPhaseListener
{
    public Inventory playerInventory;

    public void Start()
    {
        EventManager.Instance.RegisterPhaseListener(this);
        playerInventory = Inventory.Instance;
        Debug.Log("Currency spawned");
    }

    void OnDestroy()
    {
        if(EventManager.Instance != null)
        {
            EventManager.Instance.UnregisterPhaseListener(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered currency trigger");
            playerInventory.IncrementTmpCurrency();
            Destroy(this.gameObject);
        }
    }

    public void OnPhaseChangeEvent(BaseLevelStat levelStat)
    {
        if(levelStat == LevelStateManager.Instance.placementState 
            || levelStat == LevelStateManager.Instance.placementState)
        {
            Destroy(this.gameObject);
        }
    }
}
