using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int checkpointID;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SM.checkpointManager.currentCheckpoint = SM.checkpointManager.checkpoints[checkpointID];
            SM.dataManager.checkpointIndex = checkpointID;
            SM.dataManager.megos = SM.MegoManager.MegoCounter;
        }
    }
}
