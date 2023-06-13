using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource[] audioSources;

    private void Awake()
    {
        // Get all audio sources in the scene
        audioSources = FindObjectsOfType<AudioSource>();
    }

    private void Update()
    {
        // Adjust the pitch of audio sources based on the time scale
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.pitch = Time.timeScale;
        }
    }
}
