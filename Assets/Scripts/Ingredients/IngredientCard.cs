using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientCard : MonoBehaviour, IPhaseListener
{
    public Card card;
    public GameObject draggable;

    public void Start()
    {
        EventManager.Instance.RegisterPhaseListener(this);
    }

    public void OnPhaseChangeEvent(BaseLevelStat levelStat)
    {
        if (levelStat == LevelStateManager.Instance.rewardState)
        {
            card.gameObject.SetActive(true);
            card.Flip();
            Deck.Instance.AddCard(card);
            Destroy(gameObject);
        }
        if (levelStat == LevelStateManager.Instance.placementState)
        {
            card.gameObject.SetActive(true);
            draggable.SetActive(true);
            Destroy(gameObject);
        }
    }

    public void OnDestroy()
    {
        EventManager.Instance.UnregisterPhaseListener(this);
    }


}
