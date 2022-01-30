using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyCurrency : MonoBehaviour, IPhaseListener
{
    public Inventory playerInventory;


    public void Start()
    {
        EventManager.Instance.RegisterPhaseListener(this);

    }

    void OnDestroy()
    {
        if(EventManager.Instance != null)
        {
            EventManager.Instance.UnregisterPhaseListener(this);
        }
    }

    public void OnPhaseChangeEvent(BaseLevelStat levelStat)
    {
        if(levelStat == LevelStateManager.Instance.playingState)
        {
            Debug.Log("Currency Dummy Destroyed by Phase Change Event");
            Destroy(this.gameObject);
        }
    }
}
