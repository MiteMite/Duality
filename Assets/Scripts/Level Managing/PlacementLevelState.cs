using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlacementLevelState : BaseLevelStat
{

    public override void EnterState(LevelStateManager level)
    {
        
    }

    public override void UpdateState(LevelStateManager level)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            //check empty droppable
            bool allUsed = true;
            GameObject[] droppables = GameObject.FindGameObjectsWithTag("Droppable");
            for (int i = 0; i < droppables.Length; i++)
            {
                if (!DragManager.Instance.droppablesTaken.Contains(droppables[i]))
                {
                    allUsed = false;
                }
            }
            if (allUsed)
                LevelStateManager.Instance.SwitchState(LevelStateManager.Instance.playingState);
        }

        if (GameManager.Instance.lastLevel)
        {
            LevelStateManager.Instance.SwitchState(LevelStateManager.Instance.playingState);
        }
    }
}
