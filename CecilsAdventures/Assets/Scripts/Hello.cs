using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hello : MonoBehaviour
{
    public float duration;
    public GameObject helloSound;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(duration);

        Instantiate(helloSound, transform.position, transform.rotation);
    }
}
