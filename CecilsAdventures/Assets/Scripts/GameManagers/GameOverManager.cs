using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    private SceneLoader sceneLoader;

    public GameObject GameOverEffect;
    public float duration;
    public string sceneToLoad;

    public bool isGameOver;

    private void Awake()
    {
        SM.gameOverManager = this;
    }

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        isGameOver = false;
    }

    public void GameIsOver()
    {
        StartCoroutine("GameOver");
    }

    private IEnumerator GameOver()
    {
        isGameOver = true;
        SM.dataManager.megos = 0;
        SM.dataManager.checks = 0;

        Instantiate(GameOverEffect, transform.position, Quaternion.identity);           // show GAME OVER text

        yield return new WaitForSeconds(duration);

        if (sceneLoader != null)
        {
            sceneLoader.LoadScene(sceneToLoad);                          // Load the Main Menu
        }
    }

    

}
