using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    public float speed;
    public float lifespan;

    private void Start()
    {
        //GetComponent<Rigidbody2D>().velocity = transform.up * (speed);
    }

    private void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        Destroy(gameObject, lifespan);
    }


}
