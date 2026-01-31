using UnityEngine;

public class TileWood : TileBase
{
    public override void Freeze()
    {
        Change(TileType.ColdWood);
    }

    public override void Heat()
    {
        Change(TileType.BurningWood);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<TileLava>(out var lava))
        {
            Heat();
        }
    }
}
