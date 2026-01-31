using UnityEngine;

public class TileColdWood : TileBase
{
    public override void Freeze()
    {
        // Max cold
    }

    public override void Heat()
    {
        Change(TileType.Wood);
    }
}
