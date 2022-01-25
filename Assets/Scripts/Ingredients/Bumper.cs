using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    public float soundTimingReset;
    private bool m_PlayerCollision = false;
    private float m_Timer;

    private void Update()
    {
        if (m_PlayerCollision)
        {
            m_Timer += Time.deltaTime;
        }

        if (m_Timer >= soundTimingReset)
        {
            m_PlayerCollision = false;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            m_PlayerCollision = true;
            m_Timer = 0f;
        }
    }
    public bool GetBumperState()
    {
        return m_PlayerCollision;
    }
}
