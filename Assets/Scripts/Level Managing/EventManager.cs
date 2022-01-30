using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EventManager : MonoBehaviour
{
    public List<IPhaseListener> registeredPhaseListeners = new List<IPhaseListener>();
    public List<IDeathListener> registeredDeathListeners = new List<IDeathListener>();

    static EventManager m_Instance;
    static bool m_AppIsQuitting;

    private LevelStateManager m_StateManagerInstance;

    public UnityEvent m_OnCurrencyCollected;
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
                DontDestroyOnLoad(gameObjectInstance);
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

    //Listeners registration
    public void RegisterPhaseListener(IPhaseListener phaseListener)
    {
        if(!registeredPhaseListeners.Contains(phaseListener))
            registeredPhaseListeners.Add(phaseListener);
    }

    public void UnregisterPhaseListener(IPhaseListener phaseListener)
    {
        if (registeredPhaseListeners.Contains(phaseListener))
            registeredPhaseListeners.Remove(phaseListener);
    }

    public void RegisterDeathListener(IDeathListener deathListener)
    {
        registeredDeathListeners.Add(deathListener);
    }

    public void UnregisterDeathListener(IDeathListener deathListener)
    {
        registeredDeathListeners.Remove(deathListener);
    }


    //Events
    public void SendPhaseChangeEvent(BaseLevelStat levelStat)
    {
        foreach (IPhaseListener phaseListener in registeredPhaseListeners)
        {
            if(phaseListener != null)
                phaseListener.OnPhaseChangeEvent(levelStat);
        }
    }

    public void SendDeathEvent()
    {
        //Debug.Log("Event Manager Sending Death Event");
        foreach(IDeathListener deathListener in registeredDeathListeners)
        {
            if (deathListener != null)
                deathListener.OnDeathEvent();
        }
    }

    public void SendCurrencyCollectedEvent()
    {
        m_OnCurrencyCollected.Invoke();
    }

}
