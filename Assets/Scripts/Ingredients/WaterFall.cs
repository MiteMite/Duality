using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFall : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.StartWater();
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.StopWater();
        }
    }
}
