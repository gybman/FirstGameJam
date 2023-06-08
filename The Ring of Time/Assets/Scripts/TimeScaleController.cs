using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScaleController : MonoBehaviour
{
    private bool isTimeSlowed = false;
    private bool isTimeSped = false;
    private bool isTimeFrozen = false;
    private bool abilityActive;
    private Vector2 velocity;   // Keeps track of player's velocity before freezing

    [SerializeField] private float abilityDuration = 3f;
    private float timer = 0f;

    public bool slowDownEnabled = false;
    public bool speedUpEnabled = false;
    public bool stoppingEnabled = false;
    [SerializeField] private Image uiTimerBackground;
    [SerializeField] private Image uiFill;

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
            uiTimerBackground.enabled = true;
            uiFill.enabled = true;
            // Update the timer based on unscaled deltaTime
            timer -= Time.unscaledDeltaTime;

            uiFill.fillAmount = Mathf.InverseLerp(0, abilityDuration, timer);

            if (timer <= 0f)
            {
                // Time is up, return time to normal speed
                DefaultTimeScale();
            }
        }
    }

    void SlowDownTime()
    {
        if (slowDownEnabled)
        {
            DefaultTimeScale();
            Time.timeScale = 0.5f;
            isTimeSlowed = true;
        }
    }

    void SpeedUpTime()
    {
        if (speedUpEnabled)
        {
            DefaultTimeScale();
            Time.timeScale = 2f;
            isTimeSped = true;
        }
    }

    void StopTime()
    {
        if (stoppingEnabled)
        {
            DefaultTimeScale();
            //Time.timeScale = 0f;
            velocity = gameObject.GetComponent<Rigidbody2D>().velocity;
            gameObject.GetComponent<PlayerMovement>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;//simulated = false;
            gameObject.GetComponent<PickUp>().enabled = false;
            isTimeFrozen = true;
        }
    }

    void DefaultTimeScale()
    {
        Time.timeScale = 1f;
        isTimeSlowed = false;
        isTimeSped = false;
        if (isTimeFrozen)
        {
            gameObject.GetComponent<PlayerMovement>().enabled = enabled;
            gameObject.GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePosition;//simulated = true;
            gameObject.GetComponent<Rigidbody2D>().velocity = velocity;
            gameObject.GetComponent<PickUp>().enabled = true;
            isTimeFrozen = false;
        }

        timer = abilityDuration;
        uiTimerBackground.enabled = false;
        uiFill.enabled = false;
    }

    public bool GetIsTimeFrozen()
    {
        return isTimeFrozen;
    }
}
