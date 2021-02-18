using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    // to do
    // done - at the start of the game, the index is 0
    // done - at the start of a level, the index is 0
    // done - each checkpoint has a checkpointID
    // done - when a checkpoint is triggered, that checkpoint's ID is going to determine which object in the checkpoints array is the currentCheckpoint
    // done - whenever the checkpoint ID changes, it must be updated in the DataManager
    // done - whenever the game is over, the ID must be reset to 0


    public GameObject currentCheckpoint;

    public GameObject[] checkpoints;

    void Awake()
    {
        SM.checkpointManager = this;
    }

    private void Start()
    {
        currentCheckpoint = checkpoints[SM.dataManager.checkpointIndex];
    }
}
