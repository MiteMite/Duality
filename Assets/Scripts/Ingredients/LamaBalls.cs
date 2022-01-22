using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LamaBalls : MonoBehaviour
{
    public Vector3 direction;
    public void FixedUpdate()
    {
        transform.position += direction;
    }
}
