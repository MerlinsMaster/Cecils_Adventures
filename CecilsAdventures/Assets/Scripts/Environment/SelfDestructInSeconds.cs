using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructInSeconds : MonoBehaviour
{
    private ReleaseDustCloud releaseDustCloud;

    public float destroyDelay;
    public GameObject destructParticle;

    private void Start()
    {
        if (GetComponentInParent<ReleaseDustCloud>() != null)
            releaseDustCloud = GetComponentInParent<ReleaseDustCloud>();
    }

    private void Update()
    {
        if (releaseDustCloud != null)
        {
            if (releaseDustCloud.selfDestruct)
            {
                StartCoroutine(DestroySelf());
            }
        }
            
    }


    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(destroyDelay);

        Instantiate(destructParticle, transform.position, transform.rotation);
        Destroy(gameObject);
        
        yield return 0;
    }
}
