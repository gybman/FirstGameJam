using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private bool isHoldingObject = false;
    [SerializeField] private float throwForce = 5f;
    private GameObject objectToHold;

    private TimeScaleController timeController;
    [SerializeField] private GameObject[] ringSprites;

    // Start is called before the first frame update
    void Start()
    {
        timeController = GetComponent<TimeScaleController>();
        //timeController.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Object pickup
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isHoldingObject)
            {
                DropObject();
            }
            else
            {
                PickUpObject();
            }
        }
    }

    private void PickUpObject()
    {
        // Detect and pick up the nearest object within a certain range
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1.5f);
        float minDistance = float.MaxValue;
        GameObject nearestObject = null;

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Pickup"))
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestObject = collider.gameObject;
                }
            }
        }

        if (nearestObject != null)
        {
            if (nearestObject.name == "Ring")
            {
                //nearestObject.GetComponent<Collider2D>().enabled = false;
                //nearestObject.transform.SetParent(transform);
                //nearestObject.transform.localPosition = Vector3.up;
                //nearestObject.GetComponent<Rigidbody2D>().simulated = false;
                Destroy(nearestObject);
                
                //timeController.enabled = true;
                if (!timeController.slowDownEnabled)
                {
                    timeController.slowDownEnabled = true;
                    ringSprites[0].SetActive(true);
                    ringSprites[1].SetActive(true);
                    
                }else if (!timeController.speedUpEnabled)
                {
                    timeController.speedUpEnabled = true;
                    ringSprites[2].SetActive(true);
                }
                else if (!timeController.stoppingEnabled)
                {
                    timeController.stoppingEnabled = true;
                    ringSprites[3].SetActive(true);
                }

                isHoldingObject = false;
            }
            else
            {
                nearestObject.transform.SetParent(transform);
                nearestObject.transform.localPosition = Vector3.up * 5;
                nearestObject.GetComponent<Rigidbody2D>().simulated = false;
                isHoldingObject = true;
                objectToHold = nearestObject;
            }
        }
    }

    private void DropObject()
    {
        if (objectToHold != null)
        {
            Vector2 throwDirection = new Vector2(gameObject.transform.localScale.x, 0); // throws in the direction the player is facing

            // Drop the currently held object
            objectToHold.transform.SetParent(null);
            objectToHold.GetComponent<Rigidbody2D>().simulated = true;
            isHoldingObject = false;
            objectToHold.GetComponent<Rigidbody2D>().velocity = throwDirection * throwForce;    // Throws the object the player is holding
            if (objectToHold.GetComponent<EnableMovementOnFreeze>() != null) objectToHold.GetComponent<EnableMovementOnFreeze>().SetVelocity(throwDirection * throwForce);   // Stores velocity for objects that only move when frozen
            objectToHold = null;
        }
    }

    public void PlayerDied()
    {
        DropObject();
    }
}
