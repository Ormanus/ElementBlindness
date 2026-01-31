using UnityEngine;

public class TileStone : TileBase
{
    public override void Freeze()
    {
        Change(TileType.FrozenStone);
    }

    public override void Heat()
    {
        Change(TileType.HotStone);
    }
}
