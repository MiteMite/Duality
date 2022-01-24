using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanceFlamme : MonoBehaviour
{
    public GameObject flamme;
    public float upTime;
    public float downTime;

    private bool on = false;
    private float currentTime;

    public void FixedUpdate()
    {
        if (on)
        {
            if(currentTime >= upTime)
            {
                on = false;
                currentTime = 0;
                flamme.SetActive(false);
            }
            else
            {
                currentTime += Time.deltaTime;
            }
        }
        else
        {
            if (currentTime >= downTime)
            {
                on = true;
                currentTime = 0;
                flamme.SetActive(true);
            }
            else
            {
                currentTime += Time.deltaTime;
            }

        }
    }

    public bool GetFlameState() 
    {
        return on;
    }

}
