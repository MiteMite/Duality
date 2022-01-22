using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenouille : MonoBehaviour
{
    public float tongueSpeedOut;
    public float tongueSpeedIn;
    public GameObject tongue;

    private bool isTonguing;

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (!isTonguing && collision.CompareTag("Player"))
        {
            isTonguing = true;
            StartCoroutine(TongueIt(collision.transform.position));
        }
    }

    private IEnumerator TongueIt(Vector3 target)
    {
        tongue.transform.up = target - tongue.transform.position;
        while (Vector3.Distance(tongue.transform.position, target ) > tongueSpeedOut*Time.deltaTime)
        {
            tongue.transform.position = Vector3.MoveTowards(tongue.transform.position, target, tongueSpeedOut * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        while (Vector3.Distance(tongue.transform.position, transform.position) > tongueSpeedIn * Time.deltaTime)
        {
            tongue.transform.position = Vector3.MoveTowards(tongue.transform.position, transform.position, tongueSpeedIn * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        isTonguing = false;
    }
}
