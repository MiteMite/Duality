using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float force;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.GetComponent<Rigidbody2D>().velocity += (Vector2.up * force);
            collision.transform.GetComponent<PlayerController>().RemoveExtraJump();
        }
    }
}
