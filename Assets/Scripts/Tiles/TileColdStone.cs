using UnityEngine;

public class TileColdStone : TileBase
{
    public override void Freeze()
    {
        // Max cold
    }

    public override void Heat()
    {
        Change(TileType.Stone);
    }
}
