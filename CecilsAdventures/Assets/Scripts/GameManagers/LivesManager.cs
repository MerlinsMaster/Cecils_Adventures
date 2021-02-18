using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesManager : MonoBehaviour
{
    public int lifeCounter;    // current number of lives

    private void Awake()
    {
        SM.livesManager = this;
    }

    private void Start()
    {
        lifeCounter = SM.dataManager.livesLeft;
    }

    private void Update()
    {
        if (lifeCounter <= 0) // if the player has lost his last life... (you have to put "==" because it's an integer)
        {
            lifeCounter = 0;    // can't go below zero
            //SM.dataManager.checkpointIndex = 0;
        }
    }

    public void GiveLife()
    {
        lifeCounter++;  // increments lifeCounter by +1
        SM.dataManager.livesLeft = lifeCounter;
        SM.livesDisplay.UpdateLives();
    }

    public void TakeLife()
    {
        lifeCounter--;  // increments lifeCounter by -1
        SM.dataManager.livesLeft = lifeCounter;
        SM.livesDisplay.UpdateLives();

        if (lifeCounter <= 0) 
        {
            SM.gameOverManager.GameIsOver();
        }
    }
}
