using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinning : MonoBehaviour
{
    [SerializeField] private float defaultRotationSpeed = 400f; // Speed of rotation in degrees per second
    [SerializeField] private float slowedRotationSpeed = 200f;
    [SerializeField] private bool spinDirection;                // true: clockwise, false: counter-clockwise
    private bool canSpin;
    private Rigidbody2D rb;

    private void Start()
    {
        canSpin = true;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Rotates with physics so frozen balls can jam it
        float rotationSpeed = Time.timeScale == 0.5f ? slowedRotationSpeed : defaultRotationSpeed;
        if (spinDirection) rotationSpeed *= -1;     // inverts direction
        rb.angularVelocity = rotationSpeed * Mathf.Deg2Rad;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<EnableMovementOnFreeze>() != null)
        {
            canSpin = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!canSpin && collision.gameObject.GetComponent<EnableMovementOnFreeze>() != null)
        {
            canSpin = true;
        }
    }

    private void OnDisable()
    {
        rb.angularVelocity = 0;
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void OnEnable()
    {
        gameObject.GetComponent<Rigidbody2D>().constraints &= RigidbodyConstraints2D.FreezePosition;    // bitwise AND to freeze everything (since position constraints are already positive it will only freeze those and unfreeze rotation)
    }
    public bool GetCanSpin()
    {
        return canSpin;
    }
}
