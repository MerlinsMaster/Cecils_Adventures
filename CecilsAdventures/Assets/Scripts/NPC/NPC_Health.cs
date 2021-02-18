using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Health : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public GameObject deathSound;
    public GameObject deathCorpse;

    private void Update()
    {
        HealthManagement();
    }

    public void HealthManagement()
    {
        if (health <= 0)
        {
            Death();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Weapon")
        {
            if (collision.gameObject.GetComponent<Stats>() != null)
            {
                //StartCoroutine(player.Knockback(0.5f, 50, player.transform.position));
                TakeDamage(collision.gameObject.GetComponent<Stats>().damage);
            }
        }
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
    }

    public void Death()
    {
        Instantiate(deathSound, transform.position, Quaternion.identity);
        Instantiate(deathCorpse, transform.position, Quaternion.identity);
        health = maxHealth;
        Destroy(this.gameObject);  // Destroy NPC
    }
}
