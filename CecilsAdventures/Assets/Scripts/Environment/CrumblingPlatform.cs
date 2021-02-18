using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrumblingPlatform : MonoBehaviour
{
    private ReleaseDustCloud releaseDustCloud;

    private Rigidbody2D rb;
    public GameObject[] rocks;

    public float fallDelay;
    public float destructDelay;
    public float dustCloudDelay;

    private void Start()
    {
        if (GetComponentInParent<ReleaseDustCloud>() != null)
            releaseDustCloud = GetComponentInParent<ReleaseDustCloud>();

        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;

        foreach (GameObject rock in rocks)
        {
            rock.GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player" )
        {
            if (releaseDustCloud != null)
                releaseDustCloud.Invoke("SpawnDustCloud", dustCloudDelay);
            StartCoroutine(Fall());
        }
    }

    public void TriggerFall()
    {
        StartCoroutine(Fall());
    }

    public IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
        rb.isKinematic = false;
        //Destroy(gameObject, destructDelay);

        foreach (GameObject rock in rocks)
        {
            if(rock != null)
                rock.GetComponent<Rigidbody2D>().isKinematic = false;
            if(releaseDustCloud != null)
                releaseDustCloud.Invoke("SpawnDustCloud", dustCloudDelay);
            Destroy(rock, destructDelay);
        }

        yield return 0;
    }

    
}
