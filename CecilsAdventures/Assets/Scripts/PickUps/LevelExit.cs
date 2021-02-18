using System.Collections;
using UnityEngine;

public class LevelExit : PickUp
{
    private SceneLoader sceneLoader;
    private Animator anim;
    public float duration;
    public string goToScene;                    // name of scene this exit leads to

    private void Start()
    {
        anim = GetComponent<Animator>();
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public override void Collect()
    {
        StartCoroutine("EndScene");
    }

    private IEnumerator EndScene()
    {
        SM.cecilSounds.playSounds = false;
        anim.SetTrigger("PlayAnimation");       // Play animation
        SM.dataManager.checks = 0;

        yield return new WaitForSeconds(0.5f);

        TurnOffPlayer();                        // Deactivate the player

        yield return new WaitForSeconds(duration);

        sceneLoader.LoadScene(goToScene);
    }

    private void TurnOffPlayer()
    {
        SM.player.enabled = false; // player can no longer be controlled
        SM.player.playerSprites.SetActive(false);    // turns off the player renderer, making the player invisible
        SM.player.GetComponent<Collider2D>().enabled = false;
        SM.player.anim.SetFloat("Speed", 0);    // In the player's Animator, reduce the float Speed to zero so that the run sound doesn't keep playing
        SM.playerCamera.isFollowing = false;  // Takes player out of the game (camera no longer follows player)
        SM.player.rb.isKinematic = true;
        SM.player.rb.velocity = Vector3.zero; // player's rigidbody component stops moving upon death
    }
}
