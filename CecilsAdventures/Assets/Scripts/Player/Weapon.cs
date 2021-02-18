using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public GameObject projectile;
    public Transform shotPoint;
    public float coolDownAmount;

    public float coolDown;

    public float fireRate;
    private float nextFire;

    //private Animator cameraAnim;

    private void Start()
    {
        //cameraAnim = Camera.main.GetComponent<Animator>();
    }

    private void Update()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;

        if (Input.GetMouseButton(0) && Time.time > nextFire)
        {
            Instantiate(projectile, shotPoint.position, transform.rotation);
            //cameraAnim.SetTrigger("shake");
            nextFire = Time.time + fireRate;  // Cooldown timer is reset
        }

    }
}
