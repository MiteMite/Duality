using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float force;
    public float soundTimingReset;
    private bool m_PlayerCollision = false;
    private float m_Timer;

    private void Update()
    {
        if (m_PlayerCollision)
        {
            m_Timer += Time.deltaTime;
        }

        if(m_Timer >= soundTimingReset)
        {
            m_PlayerCollision = false;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            Vector2 v = collision.transform.GetComponent<Rigidbody2D>().velocity;
            v.y *= (-1) * force;
            collision.transform.GetComponent<Rigidbody2D>().velocity = v;
            Debug.Log("Player hit me");
            
            //collision.transform.GetComponent<Rigidbody2D>().velocity += (Vector2.up * force);
            m_PlayerCollision = true;
            m_Timer = 0f;
        }
    }
    public bool GetTrampolineStatus()
    {
        return m_PlayerCollision;
    }
}
