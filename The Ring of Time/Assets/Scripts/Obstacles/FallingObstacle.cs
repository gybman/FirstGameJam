using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObstacle : MonoBehaviour
{
    private Vector3 initialPosition;
    [SerializeField] private float resetZone;
    [SerializeField] private float defaultFallSpeed;
    [SerializeField] private float slowDownFallSpeed;
    private int groundLayer = 6;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float fallSpeed = Time.timeScale == 0.5f ? slowDownFallSpeed : defaultFallSpeed;

        Vector3 newVelocity = new Vector3(0f, -fallSpeed, 0f);

        GetComponent<Rigidbody2D>().velocity = newVelocity;
    }

    void ResetObject()
    {
        transform.position = initialPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == groundLayer)
        {
            ResetObject();
        }
    }
}
