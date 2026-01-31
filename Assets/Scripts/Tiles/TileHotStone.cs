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
}
