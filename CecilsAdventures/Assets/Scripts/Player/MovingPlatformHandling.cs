using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformHandling : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "MovingPlatform")
        {
            transform.parent = collision.gameObject.transform;
            player.onPlatform = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "MovingPlatform")
        {
            transform.parent = null;
            player.onPlatform = false;
        }
    }

    
}
