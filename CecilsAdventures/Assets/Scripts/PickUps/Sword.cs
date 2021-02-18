using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : PickUp
{
    public override void Collect()
    {
        SM.player.swordUnlocked = true;
        //SM.dataManager.sword = SM.player.swordUnlocked;
        base.Collect();
        Destroy(gameObject);
    }
}
