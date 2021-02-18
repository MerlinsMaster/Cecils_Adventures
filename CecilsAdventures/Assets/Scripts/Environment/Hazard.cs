using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            HazardEffect();
        }
    }

    public virtual void HazardEffect()
    {
        // Do all the things
    }
}
