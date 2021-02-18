using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject interactText;         // Text that is displayed when the player nears the interactable

    public bool playerCanInteract;          // Indicates when the player can interact with the interactable
    public bool playerHasInteracted;

    private void Start()
    {
        interactText.SetActive(false);      // Turned off by default
        playerHasInteracted = false;
    }

    private void Update()
    {
        if (playerCanInteract && Input.GetKeyDown(KeyCode.E))       // If the player is close enough and presses the E key...
        {
            playerHasInteracted = true;
            Interact();                                             // ...do the thing.
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(!playerHasInteracted)
                interactText.SetActive(true);                   // Turn on text

            playerCanInteract = true;                       // Allow player to interact
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            interactText.SetActive(false);                  // Turn off text
            playerCanInteract = false;                      // Do not allow the player to interact
        }
    }

    public virtual void Interact()
    {
        Debug.Log("Interact called.");
    }
}
