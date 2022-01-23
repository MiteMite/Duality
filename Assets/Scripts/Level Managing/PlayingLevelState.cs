using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayingLevelState : BaseLevelStat
{

    private GameObject m_Player;
    public override void EnterState(LevelStateManager level)
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_Player.GetComponent<PlayerController>().enabled = true;

    }

    public override void UpdateState(LevelStateManager level)
    {
        
    }

}
