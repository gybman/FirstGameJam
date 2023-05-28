using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float defPlayerScale;
    private int maxJumps = 2;
    private int jumpsRemaining;
    private bool isGrounded = false;
    private int groundLayer = 6;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpsRemaining = maxJumps;
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
            if (isGrounded || jumpsRemaining > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpsRemaining--;
            }
            // rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player is touching the ground
        if (collision.gameObject.layer == groundLayer)
        {
            isGrounded = true;
            jumpsRemaining = maxJumps;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == groundLayer)
        {
            isGrounded = true;
            jumpsRemaining = maxJumps;
        }
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
}
