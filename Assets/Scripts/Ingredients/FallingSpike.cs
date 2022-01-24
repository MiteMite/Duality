using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpike : MonoBehaviour
{
    public float fallSpeed;
    public float resetTime;
    public GameObject spike;

    private Vector3 originalPosition;
    private bool isFalling;

    public void Start()
    {
        originalPosition = transform.position;
    }

    private void ResetPosition()
    {
        spike.transform.position = originalPosition;
        isFalling = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isFalling && collision.CompareTag("Player"))
        {
            isFalling = true;
            StartCoroutine(Fall());
        }
    }

    private IEnumerator Fall()
    {
        float time = 0;
        while (time < resetTime)
        {
            spike.transform.position -= new Vector3(0, fallSpeed * Time.deltaTime);
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        ResetPosition();
    }

    public bool GetFallingState()
    {
        return isFalling;
    }
}
