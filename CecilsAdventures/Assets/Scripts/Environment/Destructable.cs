using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public bool resetable;

    public GameObject damageEffect;
    public GameObject destructEffect;

    public float health;
    public float maxHealth;
    public float damageAmount;      // Value to pass into TakeDamage
    public float destroyDelay;

    public string tagName;

    private void Update()
    {
        Health();
    }

    public void Health()
    {
        if (health > maxHealth) // This is here so...
            health = maxHealth; // ...the playerHealth can't go above maximum

        if (health <= 0)    // if the player is out of health points...
        {
            Destruct();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == tagName)
        {
            TakeDamage(damageAmount);
        }
    }

    public void TakeDamage(float damageTaken)
    {
        health = health - damageTaken;      // health is subtracted
        SpawnDamageEffect();
    }

    public void SpawnDamageEffect()
    {
        Instantiate(damageEffect, transform.position, transform.rotation);     // instantiate damage effect
    }

    public void SpawnDestructEffect()
    {
        Instantiate(destructEffect, transform.position, transform.rotation);     // instantiate damage effect
    }

    public void Destruct()
    {
        health = maxHealth;
        StartCoroutine(DestructCo());
    }

    public IEnumerator DestructCo()
    {
        yield return new WaitForSeconds(destroyDelay);

        SpawnDestructEffect();

        if(resetable)                               // If this gameobject is resetable...
        {
            gameObject.SetActive(false);
            //Debug.Log("object " + gameObject.name + " deactivated");
        }
        else
        {
            Destroy(gameObject);                    // Otherwise, just destroy 
        }
        
    }

    public void Reactivate()
    {
        gameObject.SetActive(true);
        //Debug.Log("object " + gameObject.name + " reactivated");
    }
}
