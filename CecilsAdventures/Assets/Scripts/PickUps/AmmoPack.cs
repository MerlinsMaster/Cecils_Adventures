using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : PickUp
{
    public int rounds;

    public override void Collect()
    {
        SM.player.ammo += rounds;    // add rounds to wherever ammo is kept track of
        base.Collect();
        Destroy(gameObject);
    }
}
