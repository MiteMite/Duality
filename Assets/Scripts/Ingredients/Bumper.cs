using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    public float force;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.transform.GetComponent<Rigidbody2D>();
            rb.velocity = collision.contacts[0].normal*force;
        }
    }
}
