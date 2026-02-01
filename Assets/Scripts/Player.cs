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

    public AudioClip hotDeathSound;

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

    void CheckFalling()
    {
        if (transform.position.y < -10)
        {
            Die();
        }
    }


    void Update()
    {
        if (Forge.IsInUse)
        {
            xSpeed = 0;
            jumpButtonPressed = false;
            HandleSprites();
            return;
        }
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
        CheckFalling();
    }

    void FixedUpdate()
    {
        const float dampingLimit = 40f;
        float vdir = Mathf.Sign(rb.linearVelocityX);
        float dampingAmount = Mathf.Min(dampingLimit, Mathf.Abs(rb.linearVelocityX) * 8f);
        rb.AddForce(new Vector2(-vdir * dampingAmount, 0));

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
            Outloud.Common.AudioManager.PlaySound(item.collectSound);
            if (item.mask)
            {
                MaskController.AddMask(item.element);
            }
            else
            {
                StoneThrowing.AddStone(item.element);
            }
            Destroy(collision.gameObject);
        }

        if (MaskController.Instance.currentElement != TileBase.Tag.Hot)
        {
            if (collision.gameObject.TryGetComponent<TileLava>(out var lava))
            {
                Outloud.Common.AudioManager.PlaySound(hotDeathSound);
                Die();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Goal>(out var goal))
        {
            goal.Enter();
        }
    }

    void Die()
    {
        DeathCanvas deathCanvas = FindFirstObjectByType<DeathCanvas>(FindObjectsInactive.Include);
        if (deathCanvas != null)
        {
            deathCanvas.gameObject.SetActive(true);
            gameObject.SetActive(false);
            MaskController.availableMasks.Clear();
            StoneThrowing.inventory.Clear();
        }
    }
}
