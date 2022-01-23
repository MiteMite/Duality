using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientCard : MonoBehaviour
{
    public GameObject card;
    public GameObject draggable;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (card)
            {
                card.SetActive(true);
                draggable.SetActive(true);
                Destroy(gameObject);
            }
        }
    }
}
