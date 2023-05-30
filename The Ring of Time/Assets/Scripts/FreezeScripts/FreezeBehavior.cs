using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeBehavior : MonoBehaviour
{
    private TimeScaleController timeFrozen;
    [SerializeField] private string[] scriptsToExclude; //Scripts to exclude
    private bool isFrozen;

    private void Start()
    {
        timeFrozen = GameObject.FindGameObjectWithTag("Player").GetComponent<TimeScaleController>();
    }
    // Update is called once per frame
    void Update()
    {
        if (timeFrozen.GetIsTimeFrozen() != isFrozen)
        {
            if (timeFrozen.GetIsTimeFrozen())
            {
                DisableAllScripts();
            }
            else
            {
                EnableAllScripts();
            }
            isFrozen = timeFrozen.GetIsTimeFrozen();
        }
    }

    private void DisableAllScripts()
    {
        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();

        for (int i = 0; i < scripts.Length; i++)
        {
            bool excludeScript = false;
            for (int j =0; j < scriptsToExclude.Length; j++)
            {
                if(scripts[i].GetType().Name == scriptsToExclude[j])
                {
                    excludeScript = true;
                    break;
                }
            }

            if (!excludeScript)
            {
                scripts[i].enabled = false;
            }
            
        }
    }

    private void EnableAllScripts()
    {
        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();

        for (int i = 0; i < scripts.Length; i++)
        {
            scripts[i].enabled = true;
        }
    }
}
