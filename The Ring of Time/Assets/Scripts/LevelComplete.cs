using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelComplete : MonoBehaviour
{
    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        // Get the current active scene
    //        Scene currentScene = SceneManager.GetActiveScene();

    //        // Load the next scene by incrementing the build index
    //        int nextSceneBuildIndex = currentScene.buildIndex + 1;
    //        SceneManager.LoadScene(nextSceneBuildIndex);
    //    }
    //}
    public Image transitionImage;
    public float transitionDuration = 1f;

    private void Awake()
    {
        transitionImage.enabled = true;
        //// Set the initial alpha of the transition image to 0
        //transitionImage.canvasRenderer.SetAlpha(0);
    }

    private void Start()
    {
        // Fade out the transition image
        transitionImage.CrossFadeAlpha(0, transitionDuration, false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TransitionRoutine());
        }
    }

    public void StartNextLevel()
    {
        StartCoroutine(TransitionRoutine());
    }

    private IEnumerator TransitionRoutine()
    {
        // Fade in the transition image
        transitionImage.canvasRenderer.SetAlpha(0);
        transitionImage.CrossFadeAlpha(1, transitionDuration, false);

        // Wait for the transition duration
        yield return new WaitForSeconds(transitionDuration);

        // Load the next scene
        LoadNextScene();
    }

    private void LoadNextScene()
    {
        // Get the current active scene
        Scene currentScene = SceneManager.GetActiveScene();

        // Load the next scene by incrementing the build index
        int nextSceneBuildIndex = currentScene.buildIndex + 1;
        SceneManager.LoadScene(nextSceneBuildIndex);
    }
}
