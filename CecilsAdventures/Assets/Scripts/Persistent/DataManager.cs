using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public int checkpointIndex;
    public int score;
    public int livesLeft;
    public int megos;
    public int checks;
    public bool gun;
    public bool sword;

    private void Awake()
    {
        SM.dataManager = this;
    }

    private void Start()
    {
        //checkpointIndex = 0;
        checks = 0;
    }
}
