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

    public Animator m_FlameAnimator;

    private void Start()
    {
        m_FlameAnimator = GetComponent<Animator>();
    }

    public void FixedUpdate()
    {
        if (on)
        {
            if(currentTime >= upTime)
            {
                on = false;
                currentTime = 0;
                flamme.SetActive(false);
                m_FlameAnimator.SetBool("_FlameOn", false);
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
                //GetComponent<SpriteRenderer>().sprite = withFlame;
                m_FlameAnimator.SetBool("_FlameOn", true);
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
