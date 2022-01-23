using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlacementLevelState : BaseLevelStat
{

    private GameObject m_Player;

    [SerializeField] private UnityEvent m_PlacementPhase;

    public override void EnterState(LevelStateManager level)
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_Player.GetComponent<PlayerController>().enabled = false;

    }

    public override void UpdateState(LevelStateManager level)
    {
        
    }
}
