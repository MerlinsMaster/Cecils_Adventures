using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseDustCloud : MonoBehaviour
{
    public GameObject dustEffect;
    public GameObject sound;
    public Transform dustcloudOrigin;

    public bool selfDestruct;

    private void Start()
    {
        selfDestruct = false;
    }

    public void SpawnDustCloud()
    {
        selfDestruct = true;
        Instantiate(dustEffect, dustcloudOrigin.transform.position, dustcloudOrigin.transform.rotation);
        Instantiate(sound, transform.position, Quaternion.identity);
    }
}
