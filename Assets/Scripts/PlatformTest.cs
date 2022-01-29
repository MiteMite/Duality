using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTest : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Platform collided");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Platform Triggered");
    }
}
