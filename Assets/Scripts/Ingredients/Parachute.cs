using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parachute : MonoBehaviour, IDeathListener
{
    LevelStateManager m_Instance;
    private void Start()
    {
        m_Instance = LevelStateManager.Instance;
        m_Instance.m_OnPhaseChangeEvent.AddListener(Activate);
        EventManager.Instance.RegisterDeathListener(this);
    }

    private void OnDestroy()
    {
        EventManager.Instance.UnregisterDeathListener(this);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.StartParachute();
            this.gameObject.SetActive(false);
            Invoke("Reactivate", 5);
        }
    }

    public void Reactivate()
    {
        this.gameObject.SetActive(true);
    }

    public void Activate(BaseLevelStat levelState)
    {
        if(levelState == LevelStateManager.Instance.placementState)
        {
            this.gameObject.SetActive(true);
        }
    }

    public void OnDeathEvent()
    {
        this.gameObject.SetActive(true);
    }
}
