using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FerrisWheel : MonoBehaviour
{
    [Range(1, 6)]
    public int platformNumber;
    public GameObject platformPrefab;
    public float speed;
    public float distance;
    public GameObject structure;

    private List<GameObject> platforms = new List<GameObject>();
    private float position;

    private void Start()
    {
        //spawn les platformes
        for (int i = 0; i < platformNumber; i++)
        {
            GameObject newPlat = Instantiate(platformPrefab, transform.position, Quaternion.identity);
            platforms.Add(newPlat);
        }
    }

    public void OnDestroy()
    {
        for (int i = 0; i < platforms.Count; i++)
        {
            Destroy(platforms[i]);
        }
    }

    public void FixedUpdate()
    {
        for (int i = 0; i < platforms.Count; i++)
        {
            Vector3 pos = transform.position + new Vector3(Mathf.Sin(position + (2*Mathf.PI / platformNumber)*i) * distance, Mathf.Cos(position + (2 * Mathf.PI / platformNumber) * i) * distance);
            platforms[i].transform.position = pos;
            position += Time.deltaTime*speed;
        }
        structure.transform.Rotate(new Vector3(0, 0, -speed *115* Time.deltaTime));
    }

}
