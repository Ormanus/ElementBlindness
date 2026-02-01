using UnityEngine;

public class TileColdWood : TileBase
{
    public override void Freeze()
    {
        // Max cold
    }

    public override void Heat()
    {
        Outloud.Common.AudioManager.PlaySound(HeatingSound);
        Change(TileType.Wood);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<TileLava>(out var lava))
        {
            Heat();
        }
    }
}
