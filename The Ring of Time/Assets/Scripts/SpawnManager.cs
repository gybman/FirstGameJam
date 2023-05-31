using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public Transform respawnPoint;
    public Vector3 cameraSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        respawnPoint = GameObject.FindGameObjectWithTag("Player").transform;
        cameraSpawnPoint = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
    }

    public void SetRespawnPoint(Transform newSpawnLocation)
    {
        respawnPoint = newSpawnLocation;
    }

    public Transform GetRespawnPoint()
    {
        return respawnPoint;
    }

    public void SetCameraSpawnPoint()
    {
        cameraSpawnPoint = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
    }

    public Vector3 GetCameraSpawnPoint()
    {
        return cameraSpawnPoint;
    }
}
