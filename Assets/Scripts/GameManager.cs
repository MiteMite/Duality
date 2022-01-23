using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private LevelStateManager lsm;
    private int currentLevel = 1;

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

    public void Start()
    {
        SceneManager.LoadScene(currentLevel);
        lsm = LevelStateManager.Instance;
    }

    public void NextScene()
    {
        currentLevel++;
        SceneManager.LoadScene(currentLevel);
        lsm.SwitchState(lsm.placementState);
    }

    public void Update()
    {

    }
}
