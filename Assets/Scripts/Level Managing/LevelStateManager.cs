using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class LevelStateManager : MonoBehaviour
{
    public BaseLevelStat m_currentState;
    public PlacementLevelState placementState = new PlacementLevelState();
    public PlayingLevelState playingState = new PlayingLevelState();
    public RewardLevelState rewardState = new RewardLevelState();


    static LevelStateManager m_Instance;
    static bool m_AppIsQuitting;

    public UnityEvent<BaseLevelStat> m_OnPhaseChangeEvent;

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
                DontDestroyOnLoad(gameObjectInstance);
                m_Instance = gameObjectInstance.GetComponent<LevelStateManager>();
                
            }
            
            return m_Instance;
        }
    }
    private void Awake()
    {
        if (m_OnPhaseChangeEvent == null)
        {
            m_OnPhaseChangeEvent = new UnityEvent<BaseLevelStat>();
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
        m_currentState = placementState;

        m_currentState.EnterState(this);
        EventManager.Instance.SendPhaseChangeEvent(placementState);
    }

    // Update is called once per frame
    void Update()
    {
        m_currentState.UpdateState(this);
        /*
        if (m_currentState == placementState && Input.GetKeyDown(KeyCode.F))
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
            if(allUsed)
                SwitchState(playingState);
        }
        else if (m_currentState == playingState && Input.GetKeyDown(KeyCode.F))
        {
            SwitchState(placementState);
        }
        else if (Input.GetKeyDown(KeyCode.V) && m_currentState == rewardState)
        {
            GameManager.Instance.NextScene();
        }
        */
        if (Input.GetKeyDown(KeyCode.P))
        {
            SwitchState(rewardState);
        }

        if (Input.GetMouseButtonDown(0) && m_currentState == rewardState)
        {
            Vector2 mmousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10));
            RaycastHit2D raycast = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 0, LayerMask.GetMask("NextLevel"));
            if(raycast.collider != null)
            {
                GameManager.Instance.NextScene();
            }
        }
    }

    public void SwitchState(BaseLevelStat state)
    {
        //Debug.Log("Changing state to " + state.GetType().ToString());
        m_currentState = state;
        state.EnterState(this);
        EventManager.Instance.SendPhaseChangeEvent(state);
        m_OnPhaseChangeEvent.Invoke(state);

    }
}
