using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    Night,
    Day
}

public abstract class CardObject : ScriptableObject
{
    
    public CardType type;

    public string cardName;
    public int pointCost;
    [TextArea(15,20)]
    public string description;

    public Sprite artwork;

    public GameObject prefab;

}
