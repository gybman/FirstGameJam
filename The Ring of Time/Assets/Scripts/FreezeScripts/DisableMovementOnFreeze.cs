using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableMovementOnFreeze : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 velocity;   // stores the velocity of the object before the freeze occurs

    // Start is called before the first frame update
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Freezes gameobject
    private void OnDisable()
    {
        velocity = rb.velocity;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }


    // Unfreezes gameObject
    private void OnEnable()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        rb.velocity = velocity;
    }
}
