using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private GateController gate;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Pickup"))
        {
            gate.PressurePlateActivated(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        gate.PressurePlateActivated(false);
    }
}
