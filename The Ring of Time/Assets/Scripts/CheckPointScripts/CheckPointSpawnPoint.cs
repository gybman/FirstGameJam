using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CheckPointSpawnPoint : MonoBehaviour
{
    private Transform spawnPoint;
    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] private CheckpointManager checkPointSave;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player") && Array.IndexOf(spawnManager.spawnPoints, gameObject.transform) > Array.IndexOf(spawnManager.spawnPoints, spawnManager.GetRespawnPoint()))
        {
            spawnManager.SetRespawnPoint(gameObject.transform);
            spawnManager.SetCameraSpawnPoint();
            checkPointSave.SavePositions();
        }
    }
}
