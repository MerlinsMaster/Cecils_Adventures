using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifespan;
    public GameObject impactEffect;
    public GameObject soundEffect;
    public GameObject ImpactSound;
    public GameObject MuzzleFlare;

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * speed;
        Instantiate(soundEffect, transform.position, Quaternion.identity);          // Instantiate the sound effect
        Instantiate(MuzzleFlare, transform.position, Quaternion.identity);          // Instantiate the muzzle flare effect
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Instantiate(impactEffect, transform.position, transform.rotation);      // Instantiate the impact effect
        Instantiate(ImpactSound, transform.position, Quaternion.identity);          // Instantiate the impact sound effect
        Destroy(gameObject);                                                        // destroy the laser bolt
    }

    private void Update()
    {
        Destroy(gameObject, lifespan);                          // Laser bolt dies once lifespan has elapsed
    }
}
