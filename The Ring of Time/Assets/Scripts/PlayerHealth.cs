using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public bool playerHealth;
    [SerializeField] private float deathFlop;
    [SerializeField] private SpawnManager spawnPoint;
    [SerializeField] private CheckpointManager checkPointSave;
    private bool waterTriggerStayAlreadyExecuted = false;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (collision.gameObject.GetComponent<Spinning>() != null)
            {
                if (collision.gameObject.GetComponent<Spinning>().GetCanSpin())
                {
                    PlayerDied();
                }
            }
            else
            {
                PlayerDied();
            }
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Water"))
    //    {
    //        PlayerDrowned();
    //        waterTriggerStayAlreadyExecuted = true;
    //    }
    //}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Water") && !waterTriggerStayAlreadyExecuted)
        {
            PlayerDrowned();
            waterTriggerStayAlreadyExecuted = true;
        }
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Water") && waterTriggerStayAlreadyExecuted)
    //    {
    //        waterTriggerStayAlreadyExecuted = false;
    //    }
    //}

    void PlayerDied()
    {
        gameObject.GetComponent<PickUp>().PlayerDied();
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        gameObject.GetComponent<PickUp>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, deathFlop);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MoveCamera>().playerDead = true;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MoveCamera>().RespawnCamera();
        Invoke("Respawn", 2f);
    }

    void PlayerDrowned()
    {
        gameObject.GetComponent<PickUp>().PlayerDied();
        // gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        gameObject.GetComponent<PickUp>().enabled = false;
        // gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, deathFlop);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MoveCamera>().playerDead = true;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MoveCamera>().RespawnCamera();
        Invoke("Respawn", 2f);
    }

    void Respawn()
    {
        Debug.Log("Respawning");
        gameObject.GetComponent<Collider2D>().enabled = true;
        gameObject.GetComponent<PlayerMovement>().enabled = true;
        gameObject.GetComponent<PickUp>().enabled = true;
        gameObject.transform.position = spawnPoint.GetRespawnPoint().position;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MoveCamera>().playerDead = false;
        if (waterTriggerStayAlreadyExecuted) waterTriggerStayAlreadyExecuted = false;
        checkPointSave.LoadPositions();
    }
}
