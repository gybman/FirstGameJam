using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    [SerializeField] private Transform leftLimit;
    [SerializeField] private Transform rightLimit;
    private bool switchSides;
    [SerializeField] private float moveSpeed;


    // Start is called before the first frame update
    void Start()
    {
        switchSides = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (switchSides)
        {
            MoveRight();
        }
        else
        {
            MoveLeft();
        }
    }

    void MoveRight()
    {
        // Move the platform to the right
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        if (transform.position.x > rightLimit.position.x)
        {
            switchSides = false;
        }
    }

    void MoveLeft()
    {
        // Move the platform to the left
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        if (transform.position.x < leftLimit.position.x)
        {
            switchSides = true;
        }
    }
}
