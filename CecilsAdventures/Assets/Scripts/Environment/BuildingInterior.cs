using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInterior : MonoBehaviour
{
    public GameObject interior;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            interior.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            interior.SetActive(false);
        }
    }

}
