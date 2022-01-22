using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public bool day;
    public float speed;
    public GameObject square;
    public Vector3 target;
    private IEnumerator moveCoroutine;
    private bool moving;

    public void MoveTo(Vector3 position)
    {
        target = position;
        if (moving)
            StopCoroutine(moveCoroutine);
        moveCoroutine = Move(position);
        StartCoroutine(moveCoroutine);
    }

    private IEnumerator Move(Vector3 position)
    {
        moving = true;
        while (Vector3.Distance(transform.position, position) > speed * Time.deltaTime)
        {
            transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        transform.position = position;
        moving = false;
    }

}
