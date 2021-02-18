using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegoManager : MonoBehaviour
{
    public int MegoCounter;

    private void Awake()
    {
        SM.MegoManager = this;
    }

    private void Start()
    {
        MegoCounter = SM.dataManager.megos;
    }

    public void GiveMego()
    {
        MegoCounter++;  
        SM.MegoDisplay.UpdateMego();
    }
}
