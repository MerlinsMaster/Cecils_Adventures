using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Doug : MonoBehaviour
{
    public float speed;
    public float moveAmount;
    public bool movingRight;

    private float timeLeft;                         // the current time in the countdown timer
    public float maxTime;                           // the maximum amount of time the countdown timer can set itself to

    public float health;
    public float maxHealth;
    public GameObject healthBar;
    public Image healthFill;

    public GameObject[] sounds;
    public GameObject[] hurtSounds;
    public int soundIndex;
    public int hurtSoundIndex;
    private float soundTimer;                         // the current time in the countdown timer
    public float maxSoundTimer;                           // the maximum amount of time the countdown timer can set itself to
    public bool playSounds;

    private Rigidbody2D rb;
    private Transform player;
    public float fireRate;
    private float nextFire;
    public GameObject projectile;
    public GameObject turret;
    public Transform shotPoint;
    public bool canFire;

    public bool isDougDead;
    public GameObject sprites;
    public GameObject DeathFlashPrefab;
    public GameObject DeathPrefab;
    public GameObject DeathSound;
    public float deathEffectDuration;
    public float endingDuration;
    public float beginningDuration;
    public bool canMove;
    public GameObject FakeDoug;
    public Transform fakeDougSpawnPoint;

    private SceneLoader sceneLoader;
    public string goToScene;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        isDougDead = false;
        sprites.SetActive(false);
        canMove = false;
        canFire = false;
        playSounds = false;
        
        CalculateDirection();

        
    }

    private void Update()
    {
        Timer();
        SoundTimer();
        HealthManagement();
        HealthBarDisplay();
        AimAtPlayer();
        FireProjectile();
    }

    private void FixedUpdate()
    {
        MoveExecution();
    }

    public void CalculateDirection()
    {
        movingRight = (Random.value > 0.5f);
        if (movingRight == true)
            moveAmount = 1;
        else
            moveAmount = -1;
    }

    public void Timer()
    {
        timeLeft -= Time.deltaTime;             // Count down at normal time
        if (timeLeft < 0)                       // if there is no time left
        {
            CalculateDirection();
            SetTimer();                         // reset the timer
        }
    }

    public void SetTimer()
    {
        timeLeft = Random.Range(0, maxTime);    // set a random number between zero and maxTime
    }

    public void SoundTimer()
    {
        soundTimer -= Time.deltaTime;             // Count down at normal time
        if (soundTimer < 0)                       // if there is no time left
        {
            DougSounds();
            SetSoundTimer();                         // reset the timer
        }
    }

    public void SetSoundTimer()
    {
        soundTimer = Random.Range(0, maxSoundTimer);    // set a random number between zero and maxTime
    }

    public void MoveExecution()
    {
        if(canMove)
            rb.velocity = new Vector2(moveAmount * speed, rb.velocity.y);
    }

    public void AimAtPlayer()
    {
        Vector2 direction = player.position - turret.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle -90, Vector3.forward);
        turret.transform.rotation = rotation;
    }

    public void FireProjectile()
    {
        if (canFire && Time.time > nextFire)
        {
            Instantiate(projectile, shotPoint.position, shotPoint.transform.rotation);
            nextFire = Time.time + fireRate;  // Cooldown timer is reset
        }
    }

    public void DougSounds()
    {
        if(playSounds)
        {
            soundIndex = Random.Range(0, sounds.Length);
            Instantiate(sounds[soundIndex], transform.position, Quaternion.identity); // instantiate audio damage effect
        }
    }

    public void HealthManagement()
    {
        if (health > maxHealth) // This is here so...
            health = maxHealth; // ...the playerHealth can't go above maximum

        if (health <= 0)
        {
            if (!isDougDead)
            {
                isDougDead = true;
                Death();
            }
        }
    }

    public void HealthBarDisplay()
    {
        healthFill.fillAmount = (health / maxHealth);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Stats>() != null)
        {
            TakeDamage(collision.gameObject.GetComponent<Stats>().damage);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        DamageEffect();
    }

    public void DamageEffect()
    {
        //Instantiate(damageEffect, transform.position, transform.rotation);     // instantiate visual damage effect
        hurtSoundIndex = Random.Range(0, hurtSounds.Length);
        Instantiate(hurtSounds[hurtSoundIndex], transform.position, Quaternion.identity); // instantiate audio damage effect
    }

    public void Death()
    {
        StartCoroutine("DeathCo");
    }

    public IEnumerator DeathCo()
    {
        sprites.SetActive(false);
        health = 0;               // playerHealth cannot go below zero
        canFire = false;
        playSounds = false;

        Instantiate(DeathFlashPrefab, transform.position, Quaternion.identity);
        Instantiate(DeathSound, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(deathEffectDuration);

        healthBar.SetActive(false);
        Instantiate(DeathPrefab, transform.position, transform.rotation);
        //Destroy(gameObject);

        yield return new WaitForSeconds(endingDuration);

        sceneLoader.LoadScene(goToScene);
    }

    public void ReleaseDoug()
    {
        StartCoroutine("EntranceCo");
    }

    public IEnumerator EntranceCo()
    {
        Instantiate(FakeDoug, fakeDougSpawnPoint.transform.position, transform.rotation);

        yield return new WaitForSeconds(beginningDuration);

        sprites.SetActive(true);
        canMove = true;
    }


}
