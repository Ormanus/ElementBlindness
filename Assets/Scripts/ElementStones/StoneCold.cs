using UnityEngine;

public class StoneCold : ElementStoneBase
{
    protected override void Effect(TileBase tile)
    {
        tile.Freeze();
        base.Effect(tile);
    }
}
