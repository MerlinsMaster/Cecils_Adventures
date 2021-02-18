using UnityEngine;

public class Coin : PickUp
{
    public override void Collect()
    {
        // player gains vlue of coin
        // play sound
        // instantiate particle system
        base.Collect();
        Destroy(gameObject);
    }
}
