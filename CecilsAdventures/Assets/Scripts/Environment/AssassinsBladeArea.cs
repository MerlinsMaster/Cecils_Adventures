using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinsBladeArea : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            SM.cecilSounds.stopSounds = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            SM.cecilSounds.stopSounds = false;
    }
}
