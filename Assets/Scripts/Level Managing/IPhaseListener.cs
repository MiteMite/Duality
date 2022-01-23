using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPhaseListener
{
    public void OnPhaseChangeEvent(BaseLevelStat levelStat);
}
