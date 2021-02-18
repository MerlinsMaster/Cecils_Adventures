using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private Player player;

    public float health;
    public float maxHealth;
    public float maxFallDistance;
    public float fallDamageModifier;
    public float fallDamage;

    public GameObject damageEffect;
    public GameObject deathSound;
    public GameObject DeathPrefab_Zombie;
    public GameObject DeathPrefab_Buzzsaw;
    public GameObject DeathPrefab_Falling;

    public enum CauseOfDeath {none, zombie, buzzsaw, falling};
    public CauseOfDeath causeOfDeath;
    public bool playerDead;

    private void Awake()
    {
        SM.playerHealth = this;
    }

    private void Start()
    {
        player = GetComponent<Player>();
        causeOfDeath = CauseOfDeath.none;
        playerDead = false;
    }

    private void Update()
    {
        HealthManagement();
    }

    public void CheckForFallDamage(float fallDistance)
    {
        if (fallDistance > maxFallDistance)
        {
            causeOfDeath = CauseOfDeath.falling;
            fallDamage = (fallDistance - maxFallDistance) * fallDamageModifier;
            TakeDamage(fallDamage);
        }
    }

    public void HealthManagement()
    {
        if (health > maxHealth) // This is here so...
            health = maxHealth; // ...the playerHealth can't go above maximum

        if (health <= 0)
        {
            if(!playerDead)
            {
                playerDead = true;
                Death();
            }
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Stats>() != null)
        {
            if (collision.transform.tag == "Weapon")
                return;

            if (collision.transform.tag == "Enemy")
                causeOfDeath = CauseOfDeath.zombie;
            if (collision.transform.tag == "Hazard")
                causeOfDeath = CauseOfDeath.falling;
            if (collision.transform.tag == "Buzzsaw")
                causeOfDeath = CauseOfDeath.buzzsaw;

            KnockbackEffect(collision.transform.position.x);
            TakeDamage(collision.gameObject.GetComponent<Stats>().damage);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        causeOfDeath = CauseOfDeath.none;
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        DamageEffect();
    }

    public void KnockbackEffect(float pos)
    {
        player.anim.SetTrigger("Hurt");
        player.knockbackCount = player.knockbackLength;

        if (pos > transform.position.x)
            player.knockFromRight = true;
        else
            player.knockFromRight = false;
    }

    public void DamageEffect()
    {
        Instantiate(damageEffect, transform.position, transform.rotation);     // instantiate visual damage effect
        Instantiate(player.hurtSound, transform.position, Quaternion.identity); // instantiate audio damage effect
    }

    public void FullHealth()
    {
        health = maxHealth;
    }

    public void Death()
    {
        Instantiate(deathSound, transform.position, Quaternion.identity);
        SM.cecilSounds.playSounds = false;
        SM.livesManager.TakeLife();   // Player loses a life upon death
        SM.MegoManager.MegoCounter = SM.dataManager.megos;
        health = 0;               // playerHealth cannot go below zero
        DeathEffect(causeOfDeath);
        SM.playerRespawn.DeactivatePlayer();
    }

    public void DeathEffect(CauseOfDeath causeOfDeath)
    {
        Instantiate(damageEffect, transform.position, transform.rotation);     // instantiate visual damage effect
        //Instantiate(player.hurtSound, transform.position, Quaternion.identity); // instantiate audio damage effect
        // instantiate dead player
        if(causeOfDeath == CauseOfDeath.zombie)
        {
            Instantiate(DeathPrefab_Zombie, transform.position, transform.rotation);
        }
        else if (causeOfDeath == CauseOfDeath.buzzsaw)
        {
            Instantiate(DeathPrefab_Buzzsaw, transform.position, transform.rotation);
        }
        else if (causeOfDeath == CauseOfDeath.falling)
        {
            Instantiate(DeathPrefab_Falling, transform.position, transform.rotation);
        }
        // instantiate "you died" message
    }
}
