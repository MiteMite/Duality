using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public Transform respawnPoint;

    public GameObject playerPrefab;
    public GameObject currentPlayer;

    public Inventory playerInventory;
    

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        playerInventory = Inventory.Instance;
    }

    public void OnPlayerDeath()
    {
        playerInventory.EmptyTmpCurrency();
        Invoke("Respawn", 1);
    }

    public void Respawn()
    {
        if(currentPlayer == null)
            currentPlayer = Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
    }
}
