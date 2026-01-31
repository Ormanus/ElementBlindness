using UnityEngine;

public class TileIce : TileBase
{
    public override void Freeze()
    {
        // Max cold
    }

    public override void Heat()
    {
        Change(TileType.Water);
    }
}
