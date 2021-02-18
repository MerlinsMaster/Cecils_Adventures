using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegoDisplay : MonoBehaviour
{
    public GameObject[] MegoGrapic;

    private void Awake()
    {
        SM.MegoDisplay = this;
    }

    private void Start()
    {
        UpdateMego();
    }

    public void UpdateMego()
    {
        for (int i = 0; i < MegoGrapic.Length; i++)
        {
            if ((SM.MegoManager.MegoCounter - 1) >= i)
            {
                MegoGrapic[i].SetActive(true);
            }
            else
            {
                MegoGrapic[i].SetActive(false);
            }
        }
    }
}
