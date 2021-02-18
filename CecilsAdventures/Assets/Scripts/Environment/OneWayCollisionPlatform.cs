using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayCollisionPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;
    public float waitTime;
    [SerializeField]
    private float currentWaitTime;
    public float resetTime;
    [SerializeField]
    private float currentResetTime;
    [SerializeField]
    private bool reseting;
    [SerializeField]
    private bool contactWithPlayer;

    private void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
        reseting = false;
        contactWithPlayer = false;
    }

    private void Update()
    {
        if(contactWithPlayer)
        {
            if (Input.GetKeyUp(KeyCode.S))
            {
                currentWaitTime = waitTime;
            }

            if (Input.GetKey(KeyCode.S))
            {
                if (currentWaitTime <= 0)
                {
                    effector.rotationalOffset = 180f;
                    currentWaitTime = waitTime;
                    reseting = true;
                }
                else
                {
                    currentWaitTime -= Time.deltaTime;
                }
            }
        }

        

        if(reseting)
        {
            currentResetTime -= Time.deltaTime;

            if(currentResetTime <= 0)
            {
                effector.rotationalOffset = 0;
                reseting = false;
                currentResetTime = resetTime;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            contactWithPlayer = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            contactWithPlayer = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            contactWithPlayer = false;
        }
    }
}
