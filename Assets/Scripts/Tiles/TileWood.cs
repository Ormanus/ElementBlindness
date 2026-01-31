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
}
