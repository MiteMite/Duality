using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Night Card", menuName = "Cards/NightCard")]
public class NightCard : CardObject
{
    public void Awake()
    {
        type = CardType.Night;
    }
}
