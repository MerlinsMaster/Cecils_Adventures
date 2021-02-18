using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : PickUp
{
    public int startingAmmo;

    public override void Collect()
    {
        SM.player.gunUnlocked = true;
        //SM.dataManager.gun = SM.player.gunUnlocked;
        SM.player.ammo = startingAmmo;
        base.Collect();
        Destroy(gameObject);
    }
}
