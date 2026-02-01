using UnityEngine;

public class ElementStoneBase : MonoBehaviour
{
    public Element ElementObject;
    bool _collided = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_collided)
            return;
        if (collision.gameObject.TryGetComponent<TileBase>(out var tile))
        {
            _collided = true;
            Effect(tile);
        }
    }

    protected virtual void Effect(TileBase tile)
    {
        Destroy(gameObject);
    }
}
