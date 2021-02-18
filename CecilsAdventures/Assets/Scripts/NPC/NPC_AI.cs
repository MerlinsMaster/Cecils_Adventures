using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_AI : MonoBehaviour
{
    public enum State { Idle, Patrol, Animal, Chase, Attack};
    public State currentState;
    public State assignedState;

    private NPC npc;                                    // Reference to the NPC controller script
    public bool movingRight;                            // If true, the NPC moves right, if false, the NPC moves left
    public Transform[] waypoints;                       // An array of waypoints
    public int currentWaypoint;                         // The index for the waypoint the the NPC is set to go to
    public bool enableWaypoints;                        // If true, NPC follows the waypoints, if false, just moves back and forth
    public GameObject player;                           // A reference to the Player

    public float distanceToPlayer;                      // Distance between the NPC and the Player
    public float chaseDistance;                         // If the Player comes within this distance, chase him
    public float attackDistance;                        // If the Player comes within this distance, attack him

    public bool attackType;                             // If true, attack type is melee
    public bool canAttack;                              // If false, attack type is ranged
    public bool isMoving;
    public bool obeyDetection;
    public bool canChangeDirection;
    public float timeBetweenDirectionChange;
    private float directionChangeCoolDown;
    public float movementTimer;
    public float timerMod;

    private void Start()
    {
        npc = GetComponent<NPC>();
        player = GameObject.FindGameObjectWithTag("Player");
        currentState = assignedState;
        canAttack = true;
        canChangeDirection = true;
        //npc.moveSpeed = 2;
        GetPlayerDistance();
    }

    private void Update()
    {
        UpdateAI(currentState);
        GetPlayerDistance();
        ChangeDirectionDelay();
    }

    public void Move()
    {
        npc.speed = npc.moveSpeed;
    }

    public void DontMove()
    {
        npc.speed = 0;
    }

    public void ChangeDirection()
    {
        if(canChangeDirection)
        {
            movingRight = !movingRight;
            canChangeDirection = false;
        }
            
    }

    public void ChangeDirectionDelay()
    {
        if(canChangeDirection == false)
        {
            directionChangeCoolDown -= Time.deltaTime;
            if(directionChangeCoolDown < 0)
            {
                canChangeDirection = true;
                directionChangeCoolDown = timeBetweenDirectionChange;
            }
        }
    }

    public void GetPlayerDistance()
    {
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
    }

    public void Jump()
    {
        if (npc.extraJumps > 0)                                     // If the "jump" button is pressed, and there are jumps left...
        {
            npc.Jump();                                                   // ...player jumps
            npc.JumpEffect();
            npc.JumpSound();
            npc.extraJumps--;                                                                           // One jump is deducted from extraJumps
        }
        else if (npc.extraJumps == 0 && npc.isGrounded == true)          // If the "jump" button is pressed, and there are no jumps left, and the player is grounded...
        {
            npc.Jump();                                                   // ...player jumps
            npc.JumpEffect();
            npc.JumpSound();
        }
    }

    public void MeleeAttack()
    {
        StartCoroutine(npc.MeleeAttack());
    }

    public void RangedAttack()
    {
        StartCoroutine(npc.RangedAttack());
    }

    public void UpdateAI(State state)
    {
        switch (state)
        {
            case State.Idle:
                AI_Idle();
                break;
            case State.Patrol:
                AI_Patrol();
                break;
            case State.Animal:
                AI_Animal();
                break;
            case State.Chase:
                AI_Chase();
                break;
            case State.Attack:
                AI_Attack();
                break;
        }
    }

    public void AI_Idle()
    {
        DontMove();

        if(distanceToPlayer < chaseDistance)                    // if player enters chase range, switch to chase
        {
            currentState = State.Chase;
        }

        if (distanceToPlayer < attackDistance)                    // if player enters attack range, switch to attack
        {
            currentState = State.Attack;
        }
    }

    public void AI_Patrol()
    {
        Move();

        if (obeyDetection)
        {
            if (npc.wallDetected)             // If NPC detects a wall or a ledge, change direction
            {
                ChangeDirection();
            }

            if (!npc.ledgeDetected)             // If NPC detects a wall or a ledge, change direction
            {
                ChangeDirection();
            }
        }

        if (distanceToPlayer < chaseDistance)                    // if player enters chase range, switch to chase
        {
            currentState = State.Chase;
        }

        if (distanceToPlayer < attackDistance)                    // if player enters attack range, switch to attack
        {
            currentState = State.Attack;
        }

        if(enableWaypoints)                                 // If the option is enabled, NPC will follow the waypoints
            FollowWaypoints();
    }

    public void AI_Animal()
    {
        if(isMoving)
        {
            Move();
            if (npc.wallDetected || !npc.ledgeDetected)             // If NPC detects a wall or a ledge, change direction
                ChangeDirection();
        }
        else
        {
            DontMove();
        }

        if(distanceToPlayer < chaseDistance)
        {
            isMoving = true;
            npc.speed = 8;

            if (transform.position.x < player.transform.position.x)         // if player is right, move left
            {
                movingRight = false;
            }
            else
            {
                movingRight = true;                                        // if player is left, move right
            }

            if (npc.wallDetected || !npc.ledgeDetected)             // If NPC detects a wall or a ledge, change direction
                ChangeDirection();
        }
        else
        {
            npc.speed = npc.moveSpeed;
        }

        movementTimer -= Time.deltaTime;

        if(movementTimer <= 0)
        {
            isMoving = (Random.value < 0.5);
            movingRight = (Random.value < 0.5);
            movementTimer = Random.Range(0, timerMod);
        }
    }

    public void FollowWaypoints()
    {
        if (transform.position.x < waypoints[currentWaypoint].transform.position.x)         // if player is right, move right
        {
            movingRight = true;
        }
        else
        {
            movingRight = false;                                        // if player is left, move left
        }

        float distanceToWaypoint = Vector2.Distance(transform.position, waypoints[currentWaypoint].position);  // Get distance to the current waypoint
        if(distanceToWaypoint < 0.9)                                                    // If NPC reaches the waypoint
        {
            currentWaypoint++;                                  // Increment the current waypoint to the next one in the array
            if (currentWaypoint > waypoints.Length - 1)         // If the waypoint chosen is beyond the array's length
                currentWaypoint = 0;                            // ...then go back to the first waypoint
        }
        
    }

    public void AI_Chase()
    {
        Move();

        if (transform.position.x < player.transform.position.x)         // if player is right, move right
        {
            movingRight = true;
        }
        else
        {
            movingRight = false;                                        // if player is left, move left
        }

        if (distanceToPlayer < attackDistance)                    // if player enters attack range, switch to attack
        {
            currentState = State.Attack;
        }

        if (distanceToPlayer > chaseDistance)                    // if player leaves chase range, switch to chase
        {
            currentState = assignedState;
        }

        
    }

    public void AI_Attack()
    {
        if (attackType && canAttack)
            MeleeAttack();             // if attack type is melee, melee attack
        else if(!attackType && canAttack)
            RangedAttack();             // if attack type is ranged, ranged attack

        if (distanceToPlayer > attackDistance)                    // if player leaves attack range, switch to chase
        {
            currentState = State.Chase;
        }
    }


}
