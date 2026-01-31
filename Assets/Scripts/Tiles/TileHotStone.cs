using UnityEngine;

public class TileHotStone : TileBase
{
    public override void Freeze()
    {
        Change(TileType.Stone);
    }

    public override void Heat()
    {
        ChangeToLiquid(TileType.Lava);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<TileWater>(out var water))
        {
            Freeze();
            water.Heat();
        }
    }
}
