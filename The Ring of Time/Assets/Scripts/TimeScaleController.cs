using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleController : MonoBehaviour
{
    private bool isTimeSlowed = false;
    private bool isTimeSped = false;
    private bool isTimeFrozen = false;
    private bool abilityActive;

    [SerializeField] private float abilityDuration = 3f;
    private float timer = 0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (!isTimeSlowed)
            {
                SlowDownTime();
            }
            else
            {
                DefaultTimeScale();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (!isTimeSped)
            {
                SpeedUpTime();
            }
            else
            {
                DefaultTimeScale();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (!isTimeFrozen)
            {
                StopTime();
            }
            else
            {
                DefaultTimeScale();
            }
        }

        // Checks to see if an ability is active
        if (isTimeSlowed || isTimeSped || isTimeFrozen)
        {
            abilityActive = true;
        }
        else
        {
            abilityActive = false;
        }

        if (abilityActive)
        {
            // Update the timer based on unscaled deltaTime
            timer -= Time.unscaledDeltaTime;
            Debug.Log("Timer: " + timer);

            if (timer <= 0f)
            {
                // Time is up, return time to normal speed
                DefaultTimeScale();
            }
        }
    }

    void SlowDownTime()
    {
        DefaultTimeScale();
        Time.timeScale = 0.5f;
        isTimeSlowed = true;
    }

    void SpeedUpTime()
    {
        DefaultTimeScale();
        Time.timeScale = 2f;
        isTimeSped = true;
    }

    void StopTime()
    {
        DefaultTimeScale();
        Time.timeScale = 0f;
        isTimeFrozen = true;
    }

    void DefaultTimeScale()
    {
        Time.timeScale = 1f;
        isTimeSlowed = false;
        isTimeSped = false;
        isTimeFrozen = false;

        timer = abilityDuration;
    }
}
