using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour, IPhaseListener
{
    //public bool day;
    public FullCard card;
    public float speed;
    [HideInInspector]
    public float flipSpeed;
    [HideInInspector]
    public int flipCount = 1;
    [HideInInspector]
    public Vector3 target;
    private IEnumerator moveCoroutine;
    private bool moving;

    [HideInInspector]
    public GameObject draggable;

    [HideInInspector]
    public bool spawned = false;

    private bool flipped;

    void Start()
    {
        EventManager.Instance.RegisterPhaseListener(this);
        GetComponent<SpriteRenderer>().sprite = card.isNight ? card.nightSide.artwork : card.daySide.artwork;
    }

    public void Update()
    {
        if (flipped)
        {
            if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftShift))
            {
                flipped = false;
                GetComponent<SpriteRenderer>().sprite = card.isNight ? card.nightSide.artwork : card.daySide.artwork;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.LeftShift))
            {
                flipped = true;
                GetComponent<SpriteRenderer>().sprite = card.isNight ? card.daySide.artwork : card.nightSide.artwork;
            }
        }
    }


    void OnDestroy()
    {
        if (EventManager.Instance != null)
        {
            EventManager.Instance.UnregisterPhaseListener(this);
        }

    }

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


    private void Spawn()
    {
        if (card == null) return;
        GameObject toSpawn = card.isNight ? card.nightSide.prefab : card.daySide.prefab;
        if (toSpawn && draggable != null && !spawned)
        {
            spawned = true;
            GameObject go = Instantiate(toSpawn, transform.position, Quaternion.identity);
            IngredientCard ic = go.AddComponent<IngredientCard>();
            ic.card = this;
            ic.draggable = draggable;
            gameObject.SetActive(false);
            draggable.SetActive(false);
        }
    }

    /*private IEnumerator Flip()
    {
        //transform.rotation = Quaternion.Euler(new Vector3(0, 0, 3));
        for (int i = 0; i < flipCount; i++)
        {
            card.isNight = !card.isNight;
            while (transform.localScale.x > Time.deltaTime * flipSpeed)
            {
                transform.localScale -= new Vector3(Time.deltaTime*flipSpeed, 0);
                yield return new WaitForEndOfFrame();
            }
            transform.localScale = new Vector2(0, 1);
            GetComponent<SpriteRenderer>().color = card.isNight? Color.blue:Color.white;
            //transform.rotation = Quaternion.Euler(new Vector3(0, 0, ((i % 2 == 0) ? -3 : 3)));
            while (transform.localScale.x < (1 - (Time.deltaTime * flipSpeed)))
            {
                transform.localScale += new Vector3(Time.deltaTime * flipSpeed, 0);
                yield return new WaitForEndOfFrame();
            }
        }
    }*/

    public void Flip()
    {
        card.isNight = !card.isNight;
        if (card.isNight && card.nightSide.artwork != null)
            GetComponent<SpriteRenderer>().sprite = card.nightSide.artwork;
        else if (!card.isNight && card.daySide.artwork != null)
            GetComponent<SpriteRenderer>().sprite = card.daySide.artwork;
    }

    public void OnPhaseChangeEvent(BaseLevelStat levelStat)
    {
        if(levelStat == LevelStateManager.Instance.playingState)
        {
            Spawn();
        }
    }

    public void setSprite()
    {
        GetComponent<SpriteRenderer>().sprite = card.isNight ? card.nightSide.artwork : card.daySide.artwork;
    }
}
