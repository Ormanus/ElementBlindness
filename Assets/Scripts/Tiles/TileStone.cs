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
}
