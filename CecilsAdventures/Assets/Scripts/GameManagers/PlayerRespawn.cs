 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private GameObject player;
    public GameObject respawnParticle;  // references the respawnParticle game object

    private SceneLoader sceneLoader;
    public string sceneToReload;

    public float respawnDelay;  // number of seconds between death and respawn

    public int pointPenaltyOnDeath; // number of points the player loses when getting killed

    private void Awake()
    {
        SM.playerRespawn = this;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        SM.player.enabled = false;  // 
        SM.player.rb.isKinematic = true;
        SM.player.playerSprites.SetActive(false);    // turns off the player renderer, making the player invisible
        Respawn();
    }

    public void Respawn()
    {
        player.transform.position = SM.checkManager.currentCheck.transform.position;
        SM.player.gunUnlocked = SM.dataManager.gun;
        SM.player.swordUnlocked = SM.dataManager.sword;
        SM.playerCamera.transform.position = player.transform.position;
        SM.playerCamera.cameraSize = SM.playerCamera.normalSize;

        SM.player.rb.isKinematic = false;
        SM.player.knockbackCount = 0;    // This is so there is no knockback upon respawning
        SM.player.enabled = true;  // player can once again be controlled
        SM.player.playerSprites.SetActive(true); // turns on the player renderer, making the player visible
        SM.player.GetComponent<Collider2D>().enabled = true;
        SM.playerHealth.FullHealth();   // restores player to full health
        SM.playerCamera.isFollowing = true;    // Puts player back into the game (camera is following player once again)
        SM.cecilSounds.playSounds = true;
        Instantiate(respawnParticle, SM.checkManager.currentCheck.transform.position, SM.checkManager.currentCheck.transform.rotation);  // Show respawn particle system
    }

    public void DeactivatePlayer()
    {
        StartCoroutine("DeactivatePlayerCo");
    }

    public IEnumerator DeactivatePlayerCo()
    {
        SM.player.enabled = false; // player can no longer be controlled
        SM.player.playerSprites.SetActive(false);    // turns off the player renderer, making the player invisible
        SM.player.GetComponent<Collider2D>().enabled = false;
        SM.playerCamera.isFollowing = false;  // Takes player out of the game (camera no longer follows player)
        SM.player.rb.isKinematic = true;
        SM.player.rb.velocity = Vector3.zero; // player's rigidbody component stops moving upon death
        SM.scoreManager.AddPoints(-pointPenaltyOnDeath);  // Player loses points upon death

        yield return new WaitForSeconds(respawnDelay);  // Delay before player respawns

        if(!SM.gameOverManager.isGameOver)
            sceneLoader.LoadScene(sceneToReload);        // reload current scene
    }

}
