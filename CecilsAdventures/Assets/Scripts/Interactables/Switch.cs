using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Interactable
{
    private Animator anim;

    public GameObject obstacle;
    public GameObject woodCrackSound;
    public GameObject boulder;
    public GameObject sound;
    public GameObject boulderSound;

    public GameObject crumblingPlatformGO;
    private CrumblingPlatform crumblingPlatform;

    private void Start()
    {
        crumblingPlatform = crumblingPlatformGO.GetComponent<CrumblingPlatform>();
        anim = GetComponent<Animator>();
    }

    public override void Interact()
    {
        anim.SetTrigger("pull");
        obstacle.SetActive(false);
        Instantiate(sound, transform.position, Quaternion.identity);
        Instantiate(woodCrackSound, transform.position, Quaternion.identity);
        crumblingPlatform.TriggerFall();

        GameObject boulderSoundInstance = Instantiate(boulderSound, transform.position, Quaternion.identity) as GameObject;
        if(boulder != null)
            boulderSoundInstance.transform.SetParent(boulder.transform);
    }
}
