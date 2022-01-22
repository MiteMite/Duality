using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingSquare : MonoBehaviour
{
    public float speed;

    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 0, speed*90 * Time.deltaTime));
    }
}
