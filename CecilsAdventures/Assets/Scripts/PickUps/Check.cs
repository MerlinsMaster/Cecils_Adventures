using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check : PickUp
{
    //public int id;

    public override void Collect()
    {
        SM.checkManager.UpdateCheck();
        Destroy(gameObject);
    }
}
