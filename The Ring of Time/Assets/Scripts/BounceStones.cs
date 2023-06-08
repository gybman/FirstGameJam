using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceStones : MonoBehaviour
{
    [SerializeField] private float bounceHeight = 1f;       // Maximum height of the bounce
    [SerializeField] private float bounceSpeed = 1f;        // Speed of the bounce

    private bool isBouncing = false;                        // Flag to track if the object is currently bouncing

    private void Start()
    {
        // Start the bouncing coroutine
        StartCoroutine(Bounce());
    }

    private IEnumerator Bounce()
    {
        // Set the initial position of the object
        Vector3 startPosition = transform.position;

        while (true)
        {
            // Calculate the target position for the bounce (up)
            Vector3 targetPosition = startPosition + Vector3.up * bounceHeight;

            // Move the object towards the target position
            while (transform.position != targetPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, bounceSpeed * Time.deltaTime);
                yield return null;
            }

            // Calculate the target position for the bounce (down)
            targetPosition = startPosition;

            // Move the object towards the target position
            while (transform.position != targetPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, bounceSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
}
