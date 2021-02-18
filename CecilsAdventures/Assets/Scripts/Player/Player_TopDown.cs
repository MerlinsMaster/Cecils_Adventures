using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_TopDown : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;

    private Rigidbody2D rb;
    private Animator anim;

    public Transform weaponPoint;

    private Vector2 moveAmount;

    public GameObject DeathPrefab;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MoveInput();
        HealthManagement();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    public void MoveInput()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveAmount = moveInput.normalized * speed;

        if (moveInput != Vector2.zero)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        if (moveInput.x < 0)                                                              // If player is moving left...
        {
            transform.eulerAngles = new Vector3(0, 180f, 0);                                        // ...rotate player 180 degrees on Y axis to be facing left
        }
        else if (moveInput.x > 0)                                                         // If player is moving right...
        {
            transform.eulerAngles = new Vector3(0, 0f, 0);                                          // ...rotate player 0 degrees on Y axis to be facing right
        }
    }

    public void Movement()
    {
        rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);
    }

    public void HealthManagement()
    {
        if (health > maxHealth) // This is here so...
            health = maxHealth; // ...the playerHealth can't go above maximum

        if (health <= 0)
        {
            Death();

        }
    }

    public void Death()
    {
        //Instantiate(deathSound, transform.position, Quaternion.identity);
        SM.cecilSounds.playSounds = false;
        SM.livesManager.TakeLife();   // Player loses a life upon death
        health = 0;               // playerHealth cannot go below zero
        Instantiate(DeathPrefab, transform.position, transform.rotation);
        // Destroy player ?
    }
}
