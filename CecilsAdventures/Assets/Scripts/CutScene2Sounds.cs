using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene2Sounds : MonoBehaviour
{
    public GameObject dropSoundPrefab;
    public GameObject insertSoundPrefab;
    public GameObject weaponSoundPrefab;

    public void PlayDropSound()
    {
        Instantiate(dropSoundPrefab, transform.position, transform.rotation);
    }

    public void PlayInsertSound()
    {
        Instantiate(insertSoundPrefab, transform.position, transform.rotation);
    }

    public void PlayWeaponSound()
    {
        Instantiate(weaponSoundPrefab, transform.position, transform.rotation);
    }
}
