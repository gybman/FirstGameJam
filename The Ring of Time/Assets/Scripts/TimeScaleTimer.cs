using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScaleTimer : MonoBehaviour
{
    [SerializeField] private Image uiFill;
    private float duration = 5f;

    private float remainingDuration; 

    // Update is called once per frame
    void Update()
    {
        if (remainingDuration > 0)
        {
            remainingDuration -= Time.unscaledDeltaTime;
        }
    }

    public void StartTimer()
    {
        remainingDuration = duration;
    }

    public void EndTimer()
    {
        remainingDuration = 0f;
    }
}
