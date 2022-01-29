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

    public Sprite noFlame;
    public Sprite withFlame;

    public void FixedUpdate()
    {
        if (on)
        {
            if(currentTime >= upTime)
            {
                on = false;
                currentTime = 0;
                flamme.SetActive(false);
                GetComponent<SpriteRenderer>().sprite = noFlame;
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
                GetComponent<SpriteRenderer>().sprite = withFlame;
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
