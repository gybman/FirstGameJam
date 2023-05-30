using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableMovementOnFreeze : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 velocity;   // stores the velocity of the object before the freeze occurs
    private bool isFrozen;

    // Start is called before the first frame update
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Unfreezes gameObject
    private void OnDisable()
    {
        rb.velocity = velocity;
        rb.constraints = RigidbodyConstraints2D.None;
    }

    // Freezes gameObject
    private void OnEnable()
    {
        velocity = rb.velocity;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void SetVelocity(Vector2 setVel)
    {
        velocity = setVel;
    }

    public bool IsFrozen()
    {
        return isFrozen;
    }
}
