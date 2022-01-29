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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Ball trigger entered by " + collision.gameObject.name);
        //Debug.Log("Ball parent is " + this.gameObject.transform.parent.gameObject.name);
        //Debug.Log("Ball collided with object with tag " + collision.gameObject.tag);

        if (!(collision.gameObject.name == this.gameObject.transform.parent.gameObject.name) 
            && !(collision.gameObject.CompareTag("Grenouille"))
            || collision.gameObject.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
        }

    }
}
