using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public Transform respawnPoint;

    public GameObject playerPrefab;
    public GameObject dummyPrefab;
    public GameObject currentPlayer;
    public GameObject currentDummy;

    public Inventory playerInventory;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        playerInventory = Inventory.Instance;

        if (currentDummy == null)
            currentDummy = Instantiate(dummyPrefab, respawnPoint.position, Quaternion.identity);
        LevelStateManager.Instance.m_OnPhaseChangeEvent.AddListener(OnPhaseChangeEvent);
    }

    public void OnPlayerDeath()
    {
        playerInventory.EmptyTmpCurrency();
        Invoke("Respawn", 1);
    }

    public void Respawn()
    {
        if (currentPlayer == null)
            currentPlayer = Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
    }

    public void OnPhaseChangeEvent(BaseLevelStat levelState)
    {
        //Debug.Log("Phase Change Event called on Level Manager");

        if (levelState == LevelStateManager.Instance.placementState)
        {
            if (currentPlayer != null)
                Destroy(currentPlayer);
            SpawnDummyPlayer();
        }
        else
        {
            //Debug.Log("Current Dummy is " + currentDummy);
            if (currentDummy != null)
                Destroy(currentDummy);
            Respawn();
        }
    }

    public void SpawnDummyPlayer()
    {
        if (currentDummy == null)
            currentDummy = Instantiate(dummyPrefab, respawnPoint.position, Quaternion.identity);
    }

}
