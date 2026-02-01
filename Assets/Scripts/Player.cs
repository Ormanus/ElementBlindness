using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 1;
    public float jumpSpeed = 10;

    public Sprite[] IdleSprites;
    public Sprite[] RunningSprites;
    public Sprite[] JumpingSprites;

    public float spriteFPS = 6;

    enum PlayerState
    {
        Idle, 
        Running, 
        Jumping
    }

    private Rigidbody2D rb;
    private float xSpeed = 0;
    private bool jumpButtonPressed = false;
    private bool jumpButtonReleased = true;
    private bool jumping = false;
    private PlayerState state = PlayerState.Idle;
    private SpriteRenderer sr;

    bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, 0.42f);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void HandleSprites()
    {
        if (!IsGrounded())
        {
            state = PlayerState.Jumping;
        }
        else if (Mathf.Abs(xSpeed) > 0.01 || Mathf.Abs(rb.linearVelocityX) > 0.01)
        {
            state = PlayerState.Running;
        }
        else
        {
            state = PlayerState.Idle;
        }

        if (xSpeed > 0.01)
        {
            sr.flipX = false;
        }
        else if (xSpeed < -0.01)
        {
            sr.flipX = true;
        }

        Sprite[] sprites = IdleSprites;
        switch (state)
        {
            case PlayerState.Idle:
                sprites = IdleSprites;
                break;
            case PlayerState.Running:
                sprites = RunningSprites;
                break;
            case PlayerState.Jumping:
                sprites = JumpingSprites;
                break;
        }
        int spriteIndex = Mathf.FloorToInt(Mathf.Repeat(Time.time * spriteFPS, sprites.Length));
        sr.sprite = sprites[spriteIndex];
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
            }
        }

        HandleSprites();
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
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<ItemPickup>(out var item))
        {
            if (item.mask)
            {
                MaskController.AddMask(item.element);
            }
            else
            {
                StoneThrowing.inventory.Add(item.element);
            }
            Destroy(collision.gameObject);
        }
    }
}
