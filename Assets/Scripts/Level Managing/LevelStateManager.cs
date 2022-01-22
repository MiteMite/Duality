using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStateManager : MonoBehaviour
{

    BaseLevelStat m_currenState;
    public PlacementLevelState placementState = new PlacementLevelState();
    public PlayingLevelState playingState = new PlayingLevelState();
    public RewardLevelState rewardState = new RewardLevelState();

    // Start is called before the first frame update
    void Start()
    {
        m_currenState = placementState;

        m_currenState.EnterState(this);

        Debug.Log("I called Entered State");
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
    }
}
