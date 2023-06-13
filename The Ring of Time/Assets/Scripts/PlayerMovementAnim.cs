using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementAnim : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float defPlayerScale;
    [SerializeField] private float raycastDistance;
    [SerializeField] private Transform abovePlayerRaycastTransform;     // stores value for raycast above head
    [SerializeField] private Transform belowPlayerRaycastTransform;     // stores value for raycast below player
    private Vector2 abovePlayerRaycastOrigin;                           // prevents player from getting unlimited jumps from ceiling
    private Vector2 belowPlayerRaycastOrigin;                           // resets jumps when player lands on something  
    private int maxJumps = 2;
    private int jumpsRemaining;
    // private bool isGrounded = false;
    private int groundLayer = 6;
    private int wallJump = 7;
    private Rigidbody2D rb;
    private GameObject lastCollisionObject;
    [SerializeField] private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        jumpsRemaining = maxJumps;
        abovePlayerRaycastOrigin = new Vector2(abovePlayerRaycastTransform.position.x, abovePlayerRaycastTransform.position.y);
        belowPlayerRaycastOrigin = new Vector2(belowPlayerRaycastTransform.position.x, belowPlayerRaycastTransform.position.y);
    }

    private void Update()
    {
        // Horizontal movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal * moveSpeed, rb.velocity.y);
        rb.velocity = movement;
        if (rb.velocity.x != 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

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
                animator.SetTrigger("Jump");
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpsRemaining--;
            }
            // rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
        if (!IsObstacleBelowPlayer())
        {
            animator.SetBool("OnGround", false);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // collision.gameObject.layer == groundLayer
        if (IsObstacleBelowPlayer() && !IsObstacleAboveHead())
        {
            // isGrounded = true;
            jumpsRemaining = maxJumps;
            animator.SetBool("OnGround", true);
        }

        if (collision.gameObject.layer == wallJump && collision.gameObject != lastCollisionObject)
        {
            if (jumpsRemaining < 2)
            {
                jumpsRemaining++;
            }
        }
        lastCollisionObject = collision.gameObject;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!IsObstacleBelowPlayer())
        {
            // isGrounded = false;
            if (jumpsRemaining > 1)
            {
                jumpsRemaining--;
            }
        }
    }

    // Makes sure the player can jump up
    private bool IsObstacleAboveHead()
    {
        bool obstacleAboveHead = false;
        abovePlayerRaycastOrigin = new Vector2(abovePlayerRaycastTransform.position.x, abovePlayerRaycastTransform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(abovePlayerRaycastOrigin, Vector2.up, raycastDistance);
        obstacleAboveHead = hit.collider != null && !hit.collider.CompareTag("Check Point");
        return obstacleAboveHead;
    }

    // Will reset jump if the player is on something
    private bool IsObstacleBelowPlayer()
    {
        bool obstacleBelowHead = false;
        belowPlayerRaycastOrigin = new Vector2(belowPlayerRaycastTransform.position.x, belowPlayerRaycastTransform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(belowPlayerRaycastOrigin, Vector2.down, raycastDistance);
        obstacleBelowHead = hit.collider != null && !hit.collider.CompareTag("Check Point");
        return obstacleBelowHead;
    }
}
