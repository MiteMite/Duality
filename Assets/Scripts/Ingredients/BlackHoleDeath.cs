using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleDeath : MonoBehaviour
{
    public BlackHole blackHole;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            blackHole.SetSuckState(false);
        }
    }
}
