using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float defPlayerScale;
    [SerializeField] private float raycastDistance;
    [SerializeField] private Transform raycastTransform;    // stores value for raycast
    private Vector2 raycastOrigin;                          // prevents player from getting unlimited jumps from ceiling
    private int maxJumps = 2;
    private int jumpsRemaining;
    private bool isGrounded = false;
    private int groundLayer = 6;
    private int wallJump = 7;
    private Rigidbody2D rb;
    private GameObject lastCollisionObject;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpsRemaining = maxJumps;
        raycastOrigin = new Vector2(raycastTransform.position.x, raycastTransform.position.y);
    }

    private void Update()
    {
        // Horizontal movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal * moveSpeed, rb.velocity.y);
        rb.velocity = movement;

        // Flip sprite if moving in the opposite direction
        if (moveHorizontal > 0)
        {
            transform.localScale = new Vector3(defPlayerScale, defPlayerScale, defPlayerScale);
        }
        else if (moveHorizontal < 0)
        {
            transform.localScale = new Vector3(-defPlayerScale, defPlayerScale, defPlayerScale);
        }

        // Jumping
        if (Input.GetButtonDown("Jump"))
        {
            if (jumpsRemaining > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpsRemaining--;
            }
            // rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == groundLayer && IsObstacleAboveHead())
        {
            isGrounded = true;
            jumpsRemaining = maxJumps;
        }

        if (collision.gameObject.layer == wallJump && collision.gameObject != lastCollisionObject)
        {
            jumpsRemaining++;
        }
        lastCollisionObject = collision.gameObject;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Check if the player is not touching the ground
        if (collision.gameObject.layer == groundLayer)
        {
            isGrounded = false;
            if(jumpsRemaining > 1)
            {
                jumpsRemaining--;
            }
        }
    }

    // Makes sure the player can jump up
    private bool IsObstacleAboveHead()
    {
        raycastOrigin = new Vector2(raycastTransform.position.x, raycastTransform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, Vector2.up, raycastDistance);
        return hit.collider == null;
    }
}
