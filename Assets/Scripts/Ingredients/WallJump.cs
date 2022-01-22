using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJump : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController player = other.collider.GetComponent<PlayerController>();
        if (player != null)
        {
            player.StartWalljump();
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        PlayerController player = other.collider.GetComponent<PlayerController>();
        if (player != null)
        {
            player.StopWalljump();
        }
    }
}
