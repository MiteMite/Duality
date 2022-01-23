using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class LevelStateManager : MonoBehaviour
{
    BaseLevelStat m_currenState;
    public PlacementLevelState placementState = new PlacementLevelState();
    public PlayingLevelState playingState = new PlayingLevelState();
    public RewardLevelState rewardState = new RewardLevelState();

    static LevelStateManager m_Instance;
    static bool m_AppIsQuitting;

    public static LevelStateManager Instance { get
        {
            if (m_AppIsQuitting)
            {
                m_Instance = null;
                return m_Instance;
            }

            else if(m_Instance == null)
            {
                GameObject gameObjectInstance = new GameObject("Level State Manager");
                gameObjectInstance.AddComponent<LevelStateManager>();
                m_Instance = gameObjectInstance.GetComponent<LevelStateManager>();
            }

            return m_Instance;
        }
    }

    private void OnDestroy()
    {
        {
            Destroy(this.gameObject);
            m_Instance = null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_currenState = placementState;

        m_currenState.EnterState(this);
        EventManager.Instance.SendPhaseChangeEvent(placementState);
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
        EventManager.Instance.SendPhaseChangeEvent(state);

    }
}
