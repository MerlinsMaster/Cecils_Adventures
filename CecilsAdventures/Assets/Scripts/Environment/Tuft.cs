using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuft : MonoBehaviour
{
    private Animator anim;
    public GameObject russleSound;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(russleSound, transform.position, Quaternion.identity);
            anim.SetTrigger("russle");
        }
    }
}
