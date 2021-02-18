using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLife : PickUp
{

    public override void Collect()
    {
        base.Collect();
        SM.livesManager.GiveLife();
        Destroy(gameObject);
    }
}
