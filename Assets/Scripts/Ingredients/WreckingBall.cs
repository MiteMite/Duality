using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WreckingBall : MonoBehaviour
{
    public GameObject ball;
    public float distance;
    public float speed;

    private bool left = false;
    private float position;

    public void Start()
    {
        position = Mathf.PI / 2;
    }


    public void FixedUpdate()
    {
        if (!left)
        {
            if (position >= 3*Mathf.PI/2 - 0.5)
            {
                left = true;
            }
            else
            {
                Vector3 pos = transform.position + new Vector3(Mathf.Sin(position) * distance, Mathf.Cos(position) * distance);
                ball.transform.position = pos;
                position += Time.deltaTime * speed;
            }
        }
        else
        {
            if (position <= Mathf.PI / 2 + 0.5)
            {
                left = false;
            }
            else
            {
                Vector3 pos = transform.position + new Vector3(Mathf.Sin(position) * distance, Mathf.Cos(position) * distance);
                ball.transform.position = pos;
                position -= Time.deltaTime * speed;
            }
        }
    }
}
