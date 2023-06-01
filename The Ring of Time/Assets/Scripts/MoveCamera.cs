using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform leftThreshold;
    [SerializeField] private Transform rightThreshold;
    [SerializeField] private Transform topThreshold;
    [SerializeField] private Transform bottomThreshold;
    [SerializeField] private SpawnManager cameraRespawn;


    private Vector3 targetPosition;
    public bool moveDone;
    public bool playerDead;

    private void Start()
    {
        targetPosition = transform.position;
        playerDead = false;
    }
    private void LateUpdate()
    {
        if (moveDone && !playerDead)    // Only shifts camera when the player is alive and moved between constraints
        {
            if (player.position.x < leftThreshold.position.x)
            {
                Debug.Log("moveDone: " + moveDone);
                MoveLeft();
            }
            else if (player.position.x > rightThreshold.position.x)
            {
                Debug.Log("moveDone: " + moveDone);
                MoveRight();
            }

            if (player.position.y > topThreshold.position.y)
            {
                Debug.Log("moveDone: " + moveDone);
                MoveUp();
            }
            else if (player.position.y < bottomThreshold.position.y)
            {
                Debug.Log("moveDone: " + moveDone);
                MoveDown();
            }
        }
        
        
        if (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            moveDone = false;
            if (!playerDead)    // Only moves camera when the player is alive
            {
                // Move camera towards target position
                transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            }
        }
        else
        {
            moveDone = true;
        }
    }

    void MoveRight()
    {
        float shiftAmount = rightThreshold.position.x - leftThreshold.position.x;
        // Calculate target position based on current camera position and shift amount
        targetPosition = transform.position + new Vector3(shiftAmount, 0f, 0f);
    }

    void MoveLeft()
    {
        float shiftAmount = leftThreshold.position.x - rightThreshold.position.x;
        // Calculate target position based on current camera position and shift amount
        targetPosition = transform.position + new Vector3(shiftAmount, 0f, 0f);
    }

    void MoveUp()
    {
        float shiftAmount = topThreshold.position.y - bottomThreshold.position.y;
        // Calculate target position based on current camera position and shift amount
        targetPosition = transform.position + new Vector3(0f, shiftAmount, 0f);
    }

    void MoveDown()
    {
        float shiftAmount = bottomThreshold.position.y - topThreshold.position.y;
        // Calculate target position based on current camera position and shift amount
        targetPosition = transform.position + new Vector3(0f, shiftAmount, 0f);
    }

    public void RespawnCamera()
    {
        targetPosition = cameraRespawn.GetCameraSpawnPoint();   // Gets location for camera at checkpoint
    }
}
