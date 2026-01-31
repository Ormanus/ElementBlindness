using UnityEngine;

public class TileStone : TileBase
{
    public override void Freeze()
    {
        Change(TileType.ColdStone);
    }

    public override void Heat()
    {
        Change(TileType.HotStone);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<TileLava>(out var lava))
        {
            Heat();
        }
    }
}
