using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class TileAnimation : MonoBehaviour
{
    public Sprite[] sprites;

    SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        _spriteRenderer.sprite = sprites[(int)(Time.time % sprites.Length)];
    }
}
