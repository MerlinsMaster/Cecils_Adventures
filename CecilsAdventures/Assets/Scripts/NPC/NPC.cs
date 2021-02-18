using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private Rigidbody2D rb;                                                                         // The player's Rigidbody2D
    [HideInInspector] public Animator anim;                                                                          // 
    private NPC_AI npc_AI;                                                                // 
    public GameObject particleFolder;

    [Header("NPC Attributes")]
    public float moveSpeed;
    public float speed;                                                                             // How fast the player moves - 10
    public float jumpForce;                                                                         // How much the player can jump - 15
    public float climbSpeed;                                                                        // 4
    public float gravityValue;                                                                      // 8
    public int extraJumps;                                                                          // 0
    public int extraJumpsValue;                                                                     // 0

    [Header("NPC Status")]
    public bool isGrounded;                                                                         // Determines whether or not the player is on the ground
    public bool onLadder;                                                                           // 
    public bool onPlatform;                                                                         // 
    public bool allowRunEffect;                                                                     // 

    [Header("Ground Detection")]
    public Transform groundCheck;                                                                   // (declared in Inpsector)
    public float checkRadius;                                                                       // 0.5
    public LayerMask whatIsGround;                                                                  // A layer that is applied to anything that is considered ground (declared in Inpsector)

    [Header("Wall Detection")]
    public Transform wallCheck;                                                                   // (declared in Inpsector)
    public float wallCheckRadius;                                                                       // 0.5
    public LayerMask whatIsWall;                                                                  // A layer that is applied to anything that is considered ground (declared in Inpsector)
    public bool wallDetected;
    public Transform ledgeCheck;
    public bool ledgeDetected;

    [HideInInspector] public float climbVelocity;                                                    // 

    [Header("Melee Attack")]
    public GameObject attackPoint;                                                                 // (declared in Inpsector)
    public float attackDuration;                                                                  // 0.2

    [Header("Ranged Attack")]
    public GameObject shot;                                                                 // (declared in Inpsector)
    public Transform firePoint;                                                                 // (declared in Inpsector)

    [Header("Knockback")]
    public float knockback;                                                                  // 10
    public float knockbackLength;                                                                  // 0.2
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
    public AudioSource run;                                                                         // 
    public AudioClip runClip;                                                                       // 

    private void Start()
    {
        npc_AI = GetComponent<NPC_AI>();                                                  // 
        rb = GetComponent<Rigidbody2D>();                                                           // A reference to the player's Rigidbody2D is cached
        anim = GetComponent<Animator>();                                                            // 
        extraJumps = extraJumpsValue;                                                               // extraJumps is set to the determined number of jumps
        rb.gravityScale = gravityValue;                                                             // 
        run = GetComponent<AudioSource>();                                                          // 
    }

    private void Update()
    {
        RunEffectCheck();
        Climb();
    }

    private void FixedUpdate()
    {
        GroundCheck();
        WallCheck();
        LedgeCheck();
        MoveExecution();
    }

    public void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);      // True if "groundCheck.position" is within "checkRadius" distance of an object with the layer "whatIsGround"
        //anim.SetBool("isGrounded", isGrounded);                                                     // The "isGrounded" animation parameter is set to true if the "isGrounded" bool in code is true

        if (onLadder)                                                                               // If the player is on a ladder...
            //anim.SetBool("isGrounded", true);                                                       // ...then the "isGrounded" animation parameter is set to true

        if (!onLadder && !onPlatform)                                                               // If the player is NOT on a ladder or a platform...
            //anim.SetFloat("VerticalSpeed", rb.velocity.y);                                          // ...then the animation parameter "VerticalSpeed" is set to the game object's vertical velocity

        if (isGrounded == true)                                                                     // If the player lands...
        {
            extraJumps = extraJumpsValue;                                                           // ...the number of extra jumps is reset
        }
    }

    public void WallCheck()
    {
        wallDetected = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);
    }

    public void LedgeCheck()
    {
        ledgeDetected = Physics2D.OverlapCircle(ledgeCheck.position, wallCheckRadius, whatIsWall);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(wallCheck.position, wallCheckRadius);
        Gizmos.DrawWireSphere(ledgeCheck.position, wallCheckRadius);
    }

    public void MoveExecution()
    {
        //anim.SetFloat("Speed", Mathf.Abs(speed));                                   // ANIMATION

        if (knockbackCount <= 0)
        {
            if(npc_AI.movingRight)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180f, 0);
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
            
        }
        else
        {
            if (knockFromRight)
                rb.velocity = new Vector2(-knockback, knockback);
            if (!knockFromRight)
                rb.velocity = new Vector2(knockback, knockback);
            knockbackCount -= Time.deltaTime;
        }
    }

    public void Climb()
    {
        //anim.SetBool("isClimbing", onLadder);                                                       // 

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

    public void LandingSound()                                                                      // LANDING EFFECT
    {
        Instantiate(landingParticle, transform.position, Quaternion.identity);                      // Instantiate landing particle
        Instantiate(landingSound, transform.position, Quaternion.identity);                         // Instantiate landing sound
    }

    public void RunSound()                                                                          // RUN SOUND
    {
        run.volume = Random.Range(0.2f, 0.4f);                                                      // Set to a random volume
        run.pitch = Random.Range(0.8f, 1.1f);                                                       // Set to a random pitch
        run.PlayOneShot(runClip);                                                                   // Play the clip
    }

    public void RunEffectCheck()                                                                    // RUN EFFECT CHECK
    {
        if (isGrounded && rb.velocity.x != 0 && allowRunEffect)                                      // If NPC is grounded AND is moving AND allowRunEffect is true
        {
            StartCoroutine("RunEffect");                                                            // Call the coroutine
            allowRunEffect = false;                                                                 // Set to false to prevent the coroutine from being called every frame
        }

        if (rb.velocity.x == 0 || !isGrounded)                                                       // If NPC is not moving OR not grounded...
        {
            StopCoroutine("RunEffect");                                                             // ...stop calling RunEffect
            allowRunEffect = true;                                                                  // Set to true to allow function to be called again
        }
    }

    private IEnumerator RunEffect()                                                                 // RUN EFFECT
    {
        while (isGrounded)                                                                          // If on the ground...
        {
            //GameObject runEffect = Instantiate(runParticle, transform.position, Quaternion.identity) as GameObject;                      // 
            //if (particleFolder != null)
            //    runEffect.transform.SetParent(particleFolder.transform);
            yield return new WaitForSeconds(0.25f);                                                 // Small pause
        }
    }

    public IEnumerator MeleeAttack()
    {
        //anim.SetTrigger("Melee");                                         // Play an attack animation
        npc_AI.canAttack = false;                                           // Set to false so that coroutine doesn't repeat every frame
        attackPoint.SetActive(true);                                        // Activate melee collider
        Instantiate(attackSound, transform.position, Quaternion.identity);  // play attack sound
        yield return new WaitForSeconds(attackDuration);                    // Wait for a fraction of a second
        attackPoint.SetActive(false);                                       // Deactivate melee collider
        npc_AI.canAttack = true;                                            // Set to true so that the coroutine can be called again
    }

    public IEnumerator RangedAttack()
    {
        //anim.SetTrigger("Fire");
        npc_AI.canAttack = false;                                       // Set to false so that coroutine doesn't repeat every frame
        Instantiate(shot, firePoint.position, firePoint.rotation);      // Fire the projectile
        yield return new WaitForSeconds(attackDuration);                // Wait for a fraction of a second
        npc_AI.canAttack = true;                                        // Set to true so that the coroutine can be called again
    }
}
