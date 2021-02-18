using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene3sounds : MonoBehaviour
{
    public GameObject footstepSoundPrefab;
    public GameObject ruffleSoundPrefab;
    public GameObject zombieSoundPrefab;
    public GameObject endCreditsSongSoundPrefab;

    public void PlayFootstepSound()
    {
        Instantiate(footstepSoundPrefab, transform.position, transform.rotation);
    }

    public void PlayRuffleSound()
    {
        Instantiate(ruffleSoundPrefab, transform.position, transform.rotation);
    }

    public void PlayZombieSound()
    {
        Instantiate(zombieSoundPrefab, transform.position, transform.rotation);
    }

    public void PlayEndCreditsSongSound()
    {
        Instantiate(endCreditsSongSoundPrefab, transform.position, transform.rotation);
    }
}
