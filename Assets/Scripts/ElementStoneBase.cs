using UnityEngine;

public class ElementStoneBase : MonoBehaviour
{
    public Element ElementObject;
    public AudioClip throwSound;
    public AudioClip crashSound;
    bool _collided = false;

    private void OnEnable()
    {
        Outloud.Common.AudioManager.PlaySound(throwSound);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_collided)
            return;
        if (collision.gameObject.TryGetComponent<TileBase>(out var tile))
        {
            Outloud.Common.AudioManager.PlaySound(crashSound);
            _collided = true;
            Effect(tile);
        }
    }

    protected virtual void Effect(TileBase tile)
    {
        Destroy(gameObject);
    }
}
