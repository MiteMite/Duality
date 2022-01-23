using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhaseDisabler : MonoBehaviour, IPhaseListener
{
    private PlayerController m_PlayerController;
    private BoxCollider2D m_BoxCollider2D;
    void Start()
    {
        m_PlayerController = GetComponent<PlayerController>();
        m_BoxCollider2D = GetComponent<BoxCollider2D>();

        EventManager.Instance.RegisterPhaseListener(this);
    }

    void OnDestroy()
    {
        if (EventManager.Instance != null)
        {
            EventManager.Instance.UnregisterPhaseListener(this);
        }
    }

    public void OnPhaseChangeEvent(BaseLevelStat levelStat)
    {


        if (m_BoxCollider2D != null && m_PlayerController != null)
        {


            if ((levelStat == LevelStateManager.Instance.placementState)
                )
            {
                m_PlayerController.enabled = false;
                //m_BoxCollider2D.enabled = false;

                
            }
            else
            {
                m_PlayerController.enabled = true;
                //m_BoxCollider2D.enabled = true;

            }
        }
    }
}
