using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildInteriorPatchManager : MonoBehaviour
{
    public GameObject patch;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            patch.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            patch.SetActive(true);
        }
    }
}
