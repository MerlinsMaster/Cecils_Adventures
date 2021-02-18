using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadZone : MonoBehaviour
{
    public GameObject asset;
    private bool loaded;

    private void Start()
    {
        loaded = false;
    }

    private void Update()
    {
        if(loaded)
        {
            asset.SetActive(true);
        }
        else
        {
            asset.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            loaded = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            loaded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            loaded = false;
        }
    }
}
