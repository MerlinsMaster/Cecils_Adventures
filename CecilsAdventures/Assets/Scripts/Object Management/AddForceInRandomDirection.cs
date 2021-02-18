using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceInRandomDirection : MonoBehaviour
{
    public float amount;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Random.onUnitSphere * amount;
    }
}
