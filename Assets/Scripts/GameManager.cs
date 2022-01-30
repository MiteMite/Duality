using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private LevelStateManager lsm;
    private int currentLevel = 1;
    public GameObject menu;

    private bool started = false;

    public static GameManager Instance { get => _instance; set => _instance = value; }

    public void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public void Update()
    {
        if (!started)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                started = true;
                Destroy(menu);
                SceneManager.LoadScene(currentLevel);
                lsm = LevelStateManager.Instance;

            }
        }
    }

    public void NextScene()
    {

        if (currentLevel == SceneManager.sceneCount)
        {
            currentLevel++;
            SceneManager.LoadScene(currentLevel);
            lsm.SwitchState(lsm.placementState);
        }
        else
            RestartGame();
    }

    public void RestartGame()
    {
        started = false;
        currentLevel = 1;

        Destroy(Inventory.Instance.gameObject);
        Destroy(SoundManager.Instance.gameObject);
        Destroy(LevelStateManager.Instance.gameObject);
        Destroy(EventManager.Instance.gameObject);


        SceneManager.LoadScene(0);
    }

}
