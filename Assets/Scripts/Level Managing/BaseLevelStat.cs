using UnityEngine;
using UnityEngine.Events;

public abstract class BaseLevelStat
{
    public abstract void EnterState(LevelStateManager level);

    public abstract void UpdateState(LevelStateManager level);
}
