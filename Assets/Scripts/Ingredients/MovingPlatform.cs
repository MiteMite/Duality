using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float distance;
    public bool horizontal;
    public float speed;

    private Vector2 start;
    private Vector2 end;
    private bool forward;


    void Start()
    {
        if (horizontal)
        {
            start = transform.position - new Vector3(distance / 2, 0, 0);
            end = transform.position + new Vector3(distance / 2, 0, 0);
        }
        else
        {
            start = transform.position - new Vector3(0, distance / 2, 0);
            end = transform.position + new Vector3(0, distance / 2, 0);
        }
        transform.position = start;
        forward = true;
    }

    void FixedUpdate()
    {
        if (forward)
        {
            if(Vector3.Distance(transform.position, end) > speed * Time.deltaTime)
            {
                transform.position = Vector3.MoveTowards(transform.position, end, speed*Time.deltaTime);
            }
            else
            {
                transform.position = end;
                forward = false;
            }
        }
        else
        {

            if (Vector3.Distance(transform.position, start) > speed * Time.deltaTime)
            {
                transform.position = Vector3.MoveTowards(transform.position, start, speed * Time.deltaTime);
            }
            else
            {
                transform.position = start;
                forward = true;
            }
        }
    }
}
