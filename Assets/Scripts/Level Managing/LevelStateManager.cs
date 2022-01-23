using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

    [System.Serializable]
    public class OnStateSwitch : UnityEvent<BaseLevelStat>
    {
    }
public class LevelStateManager : MonoBehaviour
{
    BaseLevelStat m_currenState;
    public PlacementLevelState placementState = new PlacementLevelState();
    public PlayingLevelState playingState = new PlayingLevelState();
    public RewardLevelState rewardState = new RewardLevelState();

    public OnStateSwitch onStateSwitch = new OnStateSwitch();

    // Start is called before the first frame update
    void Start()
    {
        m_currenState = placementState;

        m_currenState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        m_currenState.UpdateState(this);
    }

    public void SwitchState(BaseLevelStat state)
    {
        m_currenState = state;
        state.EnterState(this);
        onStateSwitch.Invoke(state);

    }

    public void DummyMethod()
    {

    }
}
