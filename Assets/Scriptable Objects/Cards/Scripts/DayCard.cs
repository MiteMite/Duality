using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Day Card", menuName = "Cards/DayCard")]
public class DayCard : CardObject
{
    public void Awake()
    {
        type = CardType.Day;
    }
}
