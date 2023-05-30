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
    private Vector3 targetPosition;
    public bool moveDone;

    private void Start()
    {
        targetPosition = transform.position;
    }
    private void LateUpdate()
    {
        if (moveDone)
        {
            if (player.position.x < leftThreshold.position.x)
            {
                MoveLeft();
            }
            else if (player.position.x > rightThreshold.position.x)
            {
                MoveRight();
            }

            if (player.position.y > topThreshold.position.y)
            {
                MoveUp();
            }
            else if (player.position.y < bottomThreshold.position.y)
            {
                MoveDown();
            }
        }
        
        
        if (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            moveDone = false;
            // Move camera towards target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
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
}
