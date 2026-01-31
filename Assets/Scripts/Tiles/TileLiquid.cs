using System.Collections.Generic;
using UnityEngine;

public abstract class TileLiquid : TileBase
{
    public void FreezeLiquid<T>(TileType solid) where T : TileLiquid
    {
        var lava = FindObjectsByType<T>(FindObjectsSortMode.None);
        Vector2Int tile0 = Vector2Int.RoundToInt(transform.position);
        List<T> pieces = new();
        for (int i = 0; i < lava.Length; i++)
        {
            Vector3 pos = lava[i].transform.position;
            Vector2Int tile = Vector2Int.RoundToInt(pos);
            if (tile == tile0)
            {
                pieces.Add(lava[i]);
            }
        }
        if (pieces.Count >= 12) // Allow 4 pieces to get lost
        {
            Vector3 pos = new Vector3(tile0.x, tile0.y);
            var stone = ResourceManager.Get<GameObject>(solid.ToString());
            if (stone == null) { return; }
            Instantiate(stone, pos, Quaternion.identity);
            for (int i = 0; i < pieces.Count; i++)
            {
                Destroy(pieces[i].gameObject);
            }
        }
    }
}
