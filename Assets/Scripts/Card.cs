using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public bool day;
    public float speed;
    public float flipSpeed;
    public int flipCount = 1;
    public Vector3 target;
    private IEnumerator moveCoroutine;
    private bool moving;

    public CardObject card;

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

    public void Update()
    {
        //this should be and even registering instead
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Spawn();
            StartCoroutine(Flip());
        }

    }

    private void Spawn()
    {
        if (card == null) return;
        GameObject toSpawn = card.prefab;
        if (toSpawn)
        {
            Instantiate(toSpawn, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private IEnumerator Flip()
    {
        //transform.rotation = Quaternion.Euler(new Vector3(0, 0, 3));
        for (int i = 0; i < flipCount; i++)
        {
            while(transform.localScale.x > Time.deltaTime * flipSpeed)
            {
                transform.localScale -= new Vector3(Time.deltaTime*flipSpeed, 0);
                yield return new WaitForEndOfFrame();
            }
            transform.localScale = new Vector2(0, 1);
            GetComponent<SpriteRenderer>().color = ((i%2 == 0)?Color.blue:Color.white);
            //transform.rotation = Quaternion.Euler(new Vector3(0, 0, ((i % 2 == 0) ? -3 : 3)));
            while (transform.localScale.x < (1 - (Time.deltaTime * flipSpeed)))
            {
                transform.localScale += new Vector3(Time.deltaTime * flipSpeed, 0);
                yield return new WaitForEndOfFrame();
            }
        }
        transform.localScale = new Vector2(1, 1);
    }

}
