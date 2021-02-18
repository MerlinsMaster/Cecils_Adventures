using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject pickUpSound;
    public GameObject pickUpEffect;
    public Transform effectSpawn;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Collect();
        }
    }

    public virtual void Collect()
    {
        Instantiate(pickUpSound, effectSpawn.position, Quaternion.identity);
        Instantiate(pickUpEffect, effectSpawn.position, Quaternion.identity);
    }
}
