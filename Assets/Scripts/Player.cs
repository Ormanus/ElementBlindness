using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 1;
    public float jumpSpeed = 10;

    private Rigidbody2D rb;
    private bool isOnGround = true;
    private float xSpeed = 0;
    private bool jumpButtonPressed = false;
    private bool jumpButtonReleased = true;
    private bool jumping = false;

    bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, 0.41f);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        xSpeed = Input.GetAxis("Horizontal");
        jumpButtonPressed = Input.GetButton("Jump");

        if (!jumpButtonPressed)
        {
            jumpButtonReleased = true;
            if (IsGrounded())
            {
                jumping = false;
                Debug.Log("GROUNDED");
            }
        }
    }

    void FixedUpdate()
    {
        rb.AddForce(new Vector2(xSpeed * speed, 0));
        if (jumpButtonPressed && jumpButtonReleased)
        {
            if (!jumping)
            {
                jumping = true;
                jumpButtonReleased = false;
                rb.linearVelocityY = jumpSpeed;
                Debug.Log("JUMP");
            }
        }
    }
}
