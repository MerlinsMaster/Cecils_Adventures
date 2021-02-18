using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TippingPlatform : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player" || collision.transform.tag == "TippingPlatform")
        {
            rb.isKinematic = false;            // turn on rigidbody
        }
    }
}
