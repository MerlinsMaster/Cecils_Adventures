using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunSoundHandling : MonoBehaviour
{
    public AudioSource run;                                                                         // 
    public AudioClip runClip;                                                                       // 

    private void Start()
    {
        run = GetComponent<AudioSource>();
    }

    public void RunningSound()                                                                          // 
    {
        run.volume = Random.Range(0.2f, 0.4f);                                                      // 
        run.pitch = Random.Range(0.8f, 1.1f);                                                       // 
        run.PlayOneShot(runClip);                                                                   // 
    }
}
