using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int playerHealth = 3;
    [SerializeField] private float deathFlop;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike") && collision.gameObject.GetComponent<Spinning>().GetCanSpin())
        {
            Debug.Log("Player died");
            PlayerDied();
        }
    }

    void PlayerDied()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, deathFlop);
    }
}
