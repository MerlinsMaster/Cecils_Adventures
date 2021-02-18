using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWeapon : MonoBehaviour
{
    public GameObject weapon;
    public GameObject deadWeapon;
    public bool weaponDestroyed;

    private void Start()
    {
        weaponDestroyed = false;
    }

    private void Update()
    {
        if(SM.playerHealth.playerDead)
        {
            Destroy(weapon);
            if(!weaponDestroyed)
            {
                Instantiate(deadWeapon, transform.position, transform.rotation);
                weaponDestroyed = true;
            }
                
        }
    }
}
