using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{

    private Rigidbody2D rb;                                                                         // The player's Rigidbody

    private bool isGrounded;                                                                        // Determines whether or not the player is on the ground
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;                                                                  // A layer that is applied to anything that is considered ground

    public float speed;                                                                             // How fast the player moves
    public float jumpForce;                                                                         // How much the player can jump
    private float moveInput;                                                                        // Determines player direction and speed

    private int extraJumps;
    public int extraJumpsValue;

    public bool onLadder;
    public float climbSpeed;
    private float climbVelocity;
    public float gravityValue;

    public GameObject jumpSound;
    public GameObject jumpParticle;
    public GameObject runSound;
    public GameObject runParticle;
    public AudioSource run;
    public bool allowRunEffect;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();                                                           // A reference to the player's Rigidbody2D is cached
        extraJumps = extraJumpsValue;                                                               // extraJumps is set to the determined number of jumps
        rb.gravityScale = gravityValue;
        run = GetComponent<AudioSource>();
    }


}
