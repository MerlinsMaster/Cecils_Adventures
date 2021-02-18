using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDetectionTest : MonoBehaviour
{
    private Rigidbody2D rb;

    public float moveSpeed;
    public bool moveRight;

    public Transform wallCheck;
    public float wallCheckRadius;
    public LayerMask whatIsWall;
    public bool hittingWall;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        hittingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);

        if (hittingWall)
            moveRight = !moveRight;

        if(moveRight)
        {
            transform.eulerAngles = new Vector3(0, 0f, 0);
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180f, 0);
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }
    }
}
