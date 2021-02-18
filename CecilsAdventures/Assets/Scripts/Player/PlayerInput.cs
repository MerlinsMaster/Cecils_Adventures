using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    private Player player;

    [HideInInspector] public float moveInput;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        MoveInput();
        JumpInput();
        ClimbInput();
        WeaponSelect();
        MeleeAttackInput();
        RangedAttackInput();
    }

    public void MoveInput()
    {
        moveInput = Input.GetAxis("Horizontal");                                                    // A and D controls which way the player moves
    }

    public void JumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (player.extraJumps > 0)                                     // If the "jump" button is pressed, and there are jumps left...
            {
                player.Jump();                                                   // ...player jumps
                player.JumpEffect();
                player.JumpSound();
                player.extraJumps--;                                                                           // One jump is deducted from extraJumps
            }
            else if (player.extraJumps == 0 && player.isGrounded == true)          // If the "jump" button is pressed, and there are no jumps left, and the player is grounded...
            {
                player.Jump();                                                   // ...player jumps
                player.JumpEffect();
                player.JumpSound();
            }
        }

        
    }

    public void ClimbInput()
    {
        player.climbVelocity = player.climbSpeed * Input.GetAxis("Vertical");
    }

    public void WeaponSelect()
    {
        if(player.swordUnlocked || player.gunUnlocked)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                player.weaponIndex++;
                if (player.weaponIndex > 2)
                    player.weaponIndex = 0;
                player.WeaponSelect(player.weaponIndex);
            }
        }
    }

    public void MeleeAttackInput()
    {
        if(Input.GetKeyDown(KeyCode.Return) && player.weaponIndex == 1 && player.swordUnlocked)
        {
            StartCoroutine(player.MeleeAttack());
        }
    }

    public void RangedAttackInput()
    {
        if (Input.GetKeyDown(KeyCode.Return) && player.weaponIndex == 2 && player.gunUnlocked)
        {
            player.RangedAttack();
        }
    }
}
