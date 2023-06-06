using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    private bool pressurePlateActivated;
    [SerializeField] private float openSpeed;
    [SerializeField] private float closeSpeed;
    [SerializeField] private GameObject openLimit;
    [SerializeField] private GameObject closeLimit;
    [SerializeField] private GameObject topOfGate;
    [SerializeField] private GameObject bottomOfGate;
    private bool canMove = false;
    [SerializeField] private bool switchDirection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pressurePlateActivated)
        {
            OpenGate();
        }
        else
        {
            CloseGate();
        }
    }

    public void PressurePlateActivated(bool activated)
    {
        pressurePlateActivated = activated;
    }

    void OpenGate()
    {
        if (!switchDirection)
        {
            if (canMove)
            {
                // Move the gate to its open position
                transform.Translate(Vector3.up * openSpeed * Time.deltaTime);

                // Check if the gate has reached the open limit
                if (topOfGate.transform.position.y >= openLimit.transform.position.y)
                {
                    canMove = false; // Prevent further movement
                }
            }
            else if (!canMove && topOfGate.transform.position.y < openLimit.transform.position.y)
            {
                canMove = true; // Reset canMove to true to allow opening the gate again
            }
        }
        else
        {
            if (canMove)
            {
                // Move the gate to its open position
                transform.Translate(Vector3.down * openSpeed * Time.deltaTime);

                // Check if the gate has reached the open limit
                if (topOfGate.transform.position.y <= openLimit.transform.position.y)
                {
                    canMove = false; // Prevent further movement
                }
            }
            else if (!canMove && topOfGate.transform.position.y > openLimit.transform.position.y)
            {
                canMove = true; // Reset canMove to true to allow opening the gate again
            }
        }
        
    }

    void CloseGate()
    {
        if (!switchDirection)
        {
            if (canMove)
            {
                // Move the gate to its closed position
                transform.Translate(Vector3.down * closeSpeed * Time.deltaTime);

                // Check if the gate has reached the close limit
                if (bottomOfGate.transform.position.y <= closeLimit.transform.position.y)
                {
                    canMove = false; // Prevent further movement
                }
            }
            else if (!canMove && bottomOfGate.transform.position.y > closeLimit.transform.position.y)
            {
                canMove = true; // Reset canMove to true to allow opening the gate again
            }
        }
        else
        {
            if (canMove)
            {
                // Move the gate to its closed position
                transform.Translate(Vector3.up * closeSpeed * Time.deltaTime);

                // Check if the gate has reached the close limit
                if (bottomOfGate.transform.position.y >= closeLimit.transform.position.y)
                {
                    canMove = false; // Prevent further movement
                }
            }
            else if (!canMove && bottomOfGate.transform.position.y < closeLimit.transform.position.y)
            {
                canMove = true; // Reset canMove to true to allow opening the gate again
            }
        }
    }
}
