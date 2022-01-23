using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButtonBehaviour : MonoBehaviour
{
    public void OnButtonPress()
    {
        LevelStateManager.Instance.SwitchState(LevelStateManager.Instance.playingState);
    }
}
