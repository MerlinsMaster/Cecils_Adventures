using UnityEngine;

public class Door : Interactable
{
    public GameObject locked;                   // active game object if Locked
    public GameObject unlocked;                 // active game object if Unlocked
    public GameObject open;                     // active game object if Open

    public bool isLocked;
    public bool isClosed;
}
