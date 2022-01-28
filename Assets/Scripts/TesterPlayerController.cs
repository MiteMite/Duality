using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesterPlayerController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {

            //Debug.Log("Key down");

            EventManager.Instance.SendPhaseChangeEvent(
                LevelStateManager.Instance.playingState);
        }
    }

}
