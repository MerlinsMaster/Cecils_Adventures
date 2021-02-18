using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegoEffect : MonoBehaviour
{
    private Animator anim;
    public float displayTime;

    private void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine("MegoDollEffect");
        
    }

    private IEnumerator MegoDollEffect()
    {
        yield return new WaitForSeconds(displayTime);

        anim.SetTrigger("FadeOut");                                             // fade out sprite

        yield return new WaitForSeconds(displayTime);

        Destroy(gameObject);
    }
}
