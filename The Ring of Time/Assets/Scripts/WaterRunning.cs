using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRunning : MonoBehaviour
{
    private Collider2D waterCollider;
    private BuoyancyEffector2D waterEffect;
    private bool isWaterFall = false;
    // Start is called before the first frame update
    void Start()
    {
        waterCollider = GetComponent<Collider2D>();
        waterEffect = GetComponent<BuoyancyEffector2D>();

        if (waterEffect == null) isWaterFall = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 2f)
        {
            waterCollider.isTrigger = false;
            if (!isWaterFall) waterEffect.enabled = false;

        }
        else
        {
            waterCollider.isTrigger = true;
            if (!isWaterFall) waterEffect.enabled = true;
        }
    }
}
