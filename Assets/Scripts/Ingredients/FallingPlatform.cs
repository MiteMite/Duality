using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float fallSpeed;
    public float resetTime;

    private Vector3 originalPosition;
    private bool isFalling;

    public void Start()
    {
        originalPosition = transform.position;
    }

    private void ResetPosition()
    {
        transform.position = originalPosition;
        isFalling = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isFalling && collision.collider.CompareTag("Player"))
        {
            isFalling = true;
            StartCoroutine(Fall());
        }
    }

    private IEnumerator Fall()
    {
        float time = 0;
        while(time < resetTime)
        {
            transform.position -= new Vector3(0, fallSpeed * Time.deltaTime);
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        ResetPosition();
    }


}
