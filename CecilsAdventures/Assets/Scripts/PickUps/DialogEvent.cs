using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogEvent : PickUp
{
    public GameObject dialogObject;
    public Text dialogText;
    public string textCopy;

    public bool hasBeenActivated;
    public float duration;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        dialogText.text = textCopy;
        dialogObject.SetActive(false);
        hasBeenActivated = false;
    }

    public override void Collect()
    {
        base.Collect();
        hasBeenActivated = true;
        dialogObject.SetActive(true);
        anim.SetTrigger("OpenDialog");
    }

    private void Update()
    {
        if(hasBeenActivated)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine("CloseDialogBox");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine("CloseDialogBox");
        }
    }

    private IEnumerator CloseDialogBox()
    {
        Time.timeScale = 1.0f;
        anim.SetTrigger("EndDialog");

        yield return new WaitForSeconds(duration);

        dialogObject.SetActive(false);
        Destroy(gameObject);
    }
}
