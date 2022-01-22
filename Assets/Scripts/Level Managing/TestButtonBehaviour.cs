using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButtonBehaviour : MonoBehaviour
{
    public LevelStateManager levelState;
    public void OnButtonPress()
    {
        levelState.SwitchState(levelState.playingState);
    }
}
