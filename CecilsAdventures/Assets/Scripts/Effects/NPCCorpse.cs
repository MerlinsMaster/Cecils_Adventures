using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCorpse : MonoBehaviour
{
    public float delay;

    private void Start()
    {
        if(GetComponent<Rigidbody2D>() != null)
            Destroy(GetComponent<Rigidbody2D>(), delay);
        if (GetComponent<CapsuleCollider2D>() != null)
            Destroy(GetComponent<CapsuleCollider2D>(), delay);
        if (GetComponent<CircleCollider2D>() != null)
            Destroy(GetComponent<CapsuleCollider2D>(), delay);
        if (GetComponentInChildren<ParticleSystem>() != null)
            Destroy(GetComponentInChildren<ParticleSystem>(), delay);

        Invoke("UnparentSelf", 0.1f);
    }

    private void UnparentSelf()
    {
        this.transform.parent = null;
    }
}
