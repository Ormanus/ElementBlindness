using UnityEngine;

public class StoneWater : ElementStoneBase
{
    protected override void Effect(TileBase tile)
    {
        base.Effect(tile);
        Vector2 rounded = (Vector2)Vector2Int.RoundToInt(transform.position);
        TileBase.SpawnLiquid(TileBase.TileType.Water, rounded);
    }
}
