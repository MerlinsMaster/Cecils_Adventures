using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    private SceneLoader sceneLoader;

    public float runTime;                                           // set amount of time from beginning of Fade Out to end of scene
    public string sceneToLoad;

    public float fadeTime;

    public IEnumerator Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();

        if (sceneLoader != null)
        {
            Debug.Log("end scene");
            yield return StartCoroutine(sceneLoader.Fade(0f));      // Fade "in" from black
            yield return new WaitForSeconds(runTime);               // Hold for set amount of time from beginning of Fade Out to end of scene
            sceneLoader.LoadScene(sceneToLoad);                          // Load the next scene
        }
    }

    private void Update()
    {
        ESCtoSkip();
    }

    public void ESCtoSkip()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            LoadNextScene();
        }
    }

    public void LoadNextScene()
    {
        StartCoroutine("LoadNextSceneCo");
    }

    public IEnumerator LoadNextSceneCo()
    {
        yield return StartCoroutine(sceneLoader.Fade(0f));      // Fade "in" from black
        yield return new WaitForSeconds(fadeTime);               // Hold for set amount of time from beginning of Fade Out to end of scene
        sceneLoader.LoadScene(sceneToLoad);                          // Load the next scene
    }
}
