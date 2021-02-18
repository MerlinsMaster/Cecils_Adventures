using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialGameData : MonoBehaviour
{
    public int startingLives;
    public int startingMegos;
    public int checkpointIndexStart;

    private void Start()
    {
        SM.dataManager.livesLeft = startingLives;
        SM.dataManager.megos = startingMegos;
        SM.dataManager.checkpointIndex = checkpointIndexStart;
        SM.dataManager.gun = false;
        SM.dataManager.sword = false;
    }
}
