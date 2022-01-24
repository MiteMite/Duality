using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public float speed;
    private bool m_IsSucking;

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = Vector3.MoveTowards(collision.transform.position, transform.position, speed*Time.deltaTime);
            m_IsSucking = true;
        }
    }

    public bool GetSuckState()
    {
        return m_IsSucking;
    }

    public void SetSuckState(bool SuckState)
    {
        m_IsSucking = SuckState;
    }
}
