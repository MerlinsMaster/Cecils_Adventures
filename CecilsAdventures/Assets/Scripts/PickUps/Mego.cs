using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mego : PickUp
{

    public override void Collect()
    {
        // player gains vlue of coin
        base.Collect();
        SM.MegoManager.GiveMego();
        Destroy(gameObject);
    }


}
