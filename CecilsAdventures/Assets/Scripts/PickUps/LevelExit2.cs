using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit2 : PickUp
{
    private SceneLoader sceneLoader;
    public float duration;
    public string goToScene;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public override void Collect()
    {
        StartCoroutine("EndScene");
    }

    private IEnumerator EndScene()
    {
        SM.cecilSounds.playSounds = false;
        SM.dataManager.checks = 0;


        yield return new WaitForSeconds(duration);

        sceneLoader.LoadScene(goToScene);
    }
}
