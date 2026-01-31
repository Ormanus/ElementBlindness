using UnityEngine;

public class ElementStoneBase : MonoBehaviour
{
    public Element ElementObject;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer is 0 or 4) // Default or Water
        {
            if (collision.gameObject.TryGetComponent<TileBase>(out var tile))
            {
                Effect(tile);
            }
        }
    }

    protected virtual void Effect(TileBase tile)
    {
        Destroy(gameObject);
    }
}
