using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scie : MonoBehaviour
{ 
    public GameObject ball;
    public float distance;
    public float speed;

    private float position;

    public void Start()
    {
    }


    public void FixedUpdate()
    {
        Vector3 pos = transform.position + new Vector3(Mathf.Sin(position) * distance, Mathf.Cos(position) * distance);
        ball.transform.position = pos;
        position += Time.deltaTime * speed;
    }
}