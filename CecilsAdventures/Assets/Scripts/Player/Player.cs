using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;                                                                         // The player's Rigidbody2D
    [HideInInspector] public Animator anim;                                                                          // 
    private PlayerInput playerInput;                                                                // 
    private PlayerHealth playerHealth;
    public GameObject playerSprites;
    [HideInInspector] public GameObject particleFolder;

    [Header("Player Attributes")]
    public float speed;                                                                             // How fast the player moves
    public float jumpForce;                                                                         // How much the player can jump
    public float climbSpeed;                                                                        // 
    public float gravityValue;                                                                      // 
    public int extraJumps;                                                                          // 
    public int extraJumpsValue;                                                                     // 

    [Header("Player Status")]
    public bool isGrounded;                                                                         // Determines whether or not the player is on the ground
    public bool onLadder;                                                                           // 
    public bool onPlatform;                                                                         // 
    public bool allowRunEffect;                                                                     // 

    [Header("Ground Detection")]
    public Transform groundCheck;                                                                   // 
    public float checkRadius;                                                                       // 
    public LayerMask whatIsGround;                                                                  // A layer that is applied to anything that is considered ground

    [Header("Fall Detection")]
    public float fallDistance;
    private bool firstTime;
    private bool isFalling;
    private Vector3 previousPosition;
    private float highestPosition;

    [HideInInspector]public float climbVelocity;                                                    // 

    public int weaponIndex;

    [Header("Melee Attack")]
    public bool swordUnlocked;
    public GameObject attackPoint;
    public GameObject meleeWeapon;
    public float attackDuration;

    [Header("Ranged Attack")]
    public bool gunUnlocked;
    public GameObject shot;
    public GameObject rangedWeapon;
    public Transform firePoint;
    public int ammo;

    [Header("Knockback")]
    public float knockback;
    public float knockbackLength;
    public float knockbackCount;
    public bool knockFromRight;

    [Header("Effects")]
    public GameObject jumpSound;                                                                    // 
    public GameObject jumpParticle;                                                                 // 
    public GameObject landingSound;                                                                 // 
    public GameObject landingParticle;                                                              // 
    public GameObject runSound;                                                                     // 
    public GameObject runParticle;                                                                  // 
    public GameObject attackSound;
    public GameObject hurtSound;
    public GameObject gunClickSound;
    public AudioSource run;                                                                         // 
    public AudioClip runClip;                                                                       // 

    private void Awake()
    {
        SM.player = this;
    }

    private void Start()
    {
        meleeWeapon.SetActive(false);
        rangedWeapon.SetActive(false);
        particleFolder = GameObject.Find("ParticleFolder");
        playerInput = GetComponent<PlayerInput>();                                                  // 
        playerHealth = GetComponent<PlayerHealth>();
        rb = GetComponent<Rigidbody2D>();                                                           // A reference to the player's Rigidbody2D is cached
        anim = GetComponent<Animator>();                                                            // 
        extraJumps = extraJumpsValue;                                                               // extraJumps is set to the determined number of jumps
        rb.gravityScale = gravityValue;                                                             // 
        previousPosition = transform.position;
        firstTime = true;
        isFalling = false;
        run = GetComponent<AudioSource>();                                                          // 
        weaponIndex = 0;

    }

    private void Update()
    {
        RunEffectCheck();
        Climb();
    }

    private void FixedUpdate()
    {
        GroundCheck();
        MoveExecution();
    }

    public void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);      // True if "groundCheck.position" is within "checkRadius" distance of an object with the layer "whatIsGround"
        anim.SetBool("isGrounded", isGrounded);                                                     // The "isGrounded" animation parameter is set to true if the "isGrounded" bool in code is true

        if (onLadder)                                                                               // If the player is on a ladder...
            anim.SetBool("isGrounded", true);                                                       // ...then the "isGrounded" animation parameter is set to true

        if (!onLadder && !onPlatform)                                                               // If the player is NOT on a ladder or a platform...
            anim.SetFloat("VerticalSpeed", rb.velocity.y);                                          // ...then the animation parameter "VerticalSpeed" is set to the game object's vertical velocity

        if (isGrounded == true)                                                                     // If the player lands...
        {
            extraJumps = extraJumpsValue;                                                           // ...the number of extra jumps is reset
        }
        else
        {
            if(transform.position.y < previousPosition.y && firstTime)                          // If the player is in the air, heading toward the ground, and it's the first frame this is occurring...
            {
                firstTime = false;                                                              // It's no longer the first time
                isFalling = true;                                                               // The player is classified as "falling"
                highestPosition = transform.position.y;                                         // Mark the current position to compare to when the player lands
            }

            previousPosition = transform.position;                                              // Update the previous position
        }

        if(isGrounded && isFalling && !onLadder)                                                             // When the player lands...
        {
            fallDistance = highestPosition - transform.position.y;                              // Calculate how far the player has fallen

            playerHealth.CheckForFallDamage(fallDistance);                                      // Feed that info to the health script to calculate fall damage, if any

            fallDistance = 0;                                                                   // Reset fallDistance
            isFalling = false;                                                                  // Reset isFalling
            firstTime = true;                                                                   // Reset firstTime
        }
    }

    public void MoveExecution()
    {
        //anim.SetFloat("Speed", Mathf.Abs(playerInput.moveInput));                                   // ANIMATION

        if (Mathf.Abs(playerInput.moveInput) > 0.01)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        if (knockbackCount <= 0)
        {
            rb.velocity = new Vector2(playerInput.moveInput * speed, rb.velocity.y);
        }
        else
        {
            if (knockFromRight)
                rb.velocity = new Vector2(-knockback, knockback);
            if (!knockFromRight)
                rb.velocity = new Vector2(knockback, knockback);
            knockbackCount -= Time.deltaTime;
        }

        if (playerInput.moveInput < 0)                                                              // If player is moving left...
        {
            transform.eulerAngles = new Vector3(0, 180f, 0);                                        // ...rotate player 180 degrees on Y axis to be facing left
        }
        else if (playerInput.moveInput > 0)                                                         // If player is moving right...
        {
            transform.eulerAngles = new Vector3(0, 0f, 0);                                          // ...rotate player 0 degrees on Y axis to be facing right
        }
    }

    public void Climb()
    {
        anim.SetBool("isClimbing", onLadder);                                                       // 

        if (onLadder)                                                                               // 
        {
            rb.gravityScale = 0f;                                                                   // 
            rb.velocity = new Vector2(rb.velocity.x, climbVelocity);                                // 
            anim.speed = Mathf.Abs(climbVelocity);                                                  // 
        }

        if (!onLadder)                                                                              // 
        {
            rb.gravityScale = gravityValue;                                                         // 
            anim.speed = 1;                                                                         // 
        }
    }

    public void Jump()                                                                              // 
    {
        rb.velocity = Vector2.up * jumpForce;                                                       // 
    }

    public void JumpEffect()                                                                        // 
    {
        Instantiate(jumpParticle, transform.position, Quaternion.identity);                         // 
    }

    public void JumpSound()                                                                         // 
    {
        Instantiate(jumpSound, transform.position, Quaternion.identity);                            // 
    }

    public void LandingSound()                                                                      // 
    {
        Instantiate(landingParticle, transform.position, Quaternion.identity);                      // Instantiate landing particle
        Instantiate(landingSound, transform.position, Quaternion.identity);                         // 
    }

    public void RunSound()                                                                          // 
    {
        run.volume = Random.Range(0.2f, 0.4f);                                                      // 
        run.pitch = Random.Range(0.8f, 1.1f);                                                       // 
        run.PlayOneShot(runClip);                                                                   // 
    }

    public void RunEffectCheck()                                                                    // 
    {
        if(isGrounded && rb.velocity.x != 0 && allowRunEffect)                                      // 
        {
            StartCoroutine("RunEffect");                                                            // 
            allowRunEffect = false;                                                                 // 
        }

        if(rb.velocity.x == 0 || !isGrounded)                                                       // 
        {
            StopCoroutine("RunEffect");                                                             // 
            allowRunEffect = true;                                                                  // 
        }
    }

    private IEnumerator RunEffect()                                                                 // 
    {
        while (isGrounded)                                                                          // 
        {
            GameObject runEffect = Instantiate(runParticle, transform.position, Quaternion.identity) as GameObject;                      // 
            if(particleFolder != null)
                runEffect.transform.SetParent(particleFolder.transform);
            yield return new WaitForSeconds(0.25f);                                                 // 
        }
    }

    public void WeaponSelect(int weapon)
    {
        if (weapon == 1)
        {
            meleeWeapon.SetActive(true);
            rangedWeapon.SetActive(false);
        }
        else if (weapon == 2)
        {
            meleeWeapon.SetActive(false);
            rangedWeapon.SetActive(true);
        }
        else 
        {
            meleeWeapon.SetActive(false);
            rangedWeapon.SetActive(false);
        }
    }

    public IEnumerator MeleeAttack()
    {
        anim.SetTrigger("Melee");                           // Play an attack animation
        attackPoint.SetActive(true);                        // Activate melee collider
        Instantiate(attackSound, transform.position, Quaternion.identity);                            // play attack sound
        yield return new WaitForSeconds(attackDuration);    // Wait for a fraction of a second
        attackPoint.SetActive(false);                       // Deactivate melee collider
    }

    public void RangedAttack()
    {
        anim.SetTrigger("Fire");
        if(ammo > 0)
        {
            Instantiate(shot, firePoint.position, firePoint.rotation);        // Fire the projectile
            ammo -= 1;
        }
        else
        {
            Instantiate(gunClickSound, transform.position, Quaternion.identity);      // play click sound
        }
        
    }


}
