using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainSpawner : MonoBehaviour
{
    public float timer;
    public float maxTimer;
    public float startingTimer;

    public GameObject train;
    public Transform spawnPoint;

    private void Start()
    {
        timer = startingTimer;
    }

    private void Update()
    {
        if(timer <= 0)
        {
            Instantiate(train, spawnPoint.position, transform.rotation);
            timer = maxTimer;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
