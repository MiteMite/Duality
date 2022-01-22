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
    public GameObject prefab;
    public CardType type;
    [TextArea(15,20)]
    public string description;

}
