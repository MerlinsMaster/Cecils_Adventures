using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckManager : MonoBehaviour
{
    public int checkCounter;
    public GameObject[] checkGOs;
    public Transform[] checkLocations;
    public Transform currentCheck;

    private void Awake()
    {
        SM.checkManager = this;
        currentCheck = checkLocations[SM.dataManager.checks];
    }

    private void Start()
    {
        checkCounter = SM.dataManager.checks;
        DestroyUnlockedChecks();
        //currentCheck = checkLocations[SM.dataManager.checks];
    }

    public void UpdateCheck()
    {
        checkCounter++;
        currentCheck = checkLocations[checkCounter];
        SM.dataManager.checks = checkCounter;
        SM.dataManager.megos = SM.MegoManager.MegoCounter;
        SM.dataManager.sword = SM.player.swordUnlocked;
        SM.dataManager.gun = SM.player.gunUnlocked;
    }

    public void DestroyUnlockedChecks()
    {
        for (int i = 0; i < checkGOs.Length; i++)
        {
            if(i < checkCounter)
            {
                Destroy(checkGOs[i]);
            }
        }
    }
}
