using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardLevelState : BaseLevelStat
{


    public override void EnterState(LevelStateManager level)
    {
        Debug.Log("The value of your deck is : " + Inventory.Instance.DeckValue());
        Debug.Log("You're a true Crazy Cat !");
    }

    public override void UpdateState(LevelStateManager level)
    {
        
    }
}
