using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EventManager : MonoBehaviour
{
    public List<IPhaseListener> registeredPhaseListeners = new List<IPhaseListener>();
    
    static EventManager m_Instance;
    static bool m_AppIsQuitting;

    private LevelStateManager m_StateManagerInstance;
    public static EventManager Instance { get
        {
            if (m_AppIsQuitting)
            {
                m_Instance = null;
                return m_Instance;
            }

            else if(m_Instance == null)
            {
                GameObject gameObjectInstance = new GameObject("Event Manager");
                gameObjectInstance.AddComponent<EventManager>();
                m_Instance = gameObjectInstance.GetComponent<EventManager>();
            }

            return m_Instance;
        }
    }

    private void Start()
    {
        m_StateManagerInstance = LevelStateManager.Instance;
    }
    public void OnDestoy()
    {
        Destroy(this.gameObject);
        m_Instance = null;
    }

    public void OnApplicationQuit()
    {
        m_AppIsQuitting = true;
    }

    public void RegisterPhaseListener(IPhaseListener phaseListener)
    {
        registeredPhaseListeners.Add(phaseListener);
    }

    public void UnregisterPhaseListener(IPhaseListener phaseListener)
    {
        registeredPhaseListeners.Remove(phaseListener);
    }

    public void SendPhaseChangeEvent(BaseLevelStat levelStat)
    {
        foreach(IPhaseListener phaseListener in registeredPhaseListeners)
        {
            phaseListener.OnPhaseChangeEvent(levelStat);
        }
    }

}
