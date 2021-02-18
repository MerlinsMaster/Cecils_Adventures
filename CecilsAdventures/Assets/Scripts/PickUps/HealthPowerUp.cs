using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : PickUp
{
    public float amount;

    public override void Collect()
    {
        SM.playerHealth.health += amount;
        base.Collect();
        Destroy(gameObject);
    }
}
