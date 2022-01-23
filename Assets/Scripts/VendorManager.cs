using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendorManager : MonoBehaviour, IPhaseListener
{
    public FullCard[] levelVendorCards = new FullCard[3];

    public void OnPhaseChangeEvent(BaseLevelStat levelStat)
    {
        if (levelStat == LevelStateManager.Instance.rewardState)
        {
            //spawn les cartes
        }
    }

    public void Start()
    {
        EventManager.Instance.RegisterPhaseListener(this);
    }
}
