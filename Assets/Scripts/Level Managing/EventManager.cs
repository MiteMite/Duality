using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum UnityEvent
{
    placementPhaseEvent, 
    playingPhaseEvent,
    RewardPhaseEvent
}
public class EventManager : MonoBehaviour
{
    static EventManager m_Instance;
    static bool m_AppIsQuitting;
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

    public void OnDestoy()
    {
        Destroy(this.gameObject);
        m_Instance = null;
    }

    public void OnApplicationQuit()
    {
        m_AppIsQuitting = true;
    }




}
