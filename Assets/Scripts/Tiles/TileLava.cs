using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileLava : TileBase
{
    private void FixedUpdate()
    {
        var rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Random.insideUnitCircle * 2f);
    }

    public override void Freeze()
    {
        var lava = FindObjectsByType<TileLava>(FindObjectsSortMode.None);
        Vector2Int tile0 = Vector2Int.RoundToInt(transform.position);
        List<TileLava> pieces = new();
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
            var stone = ResourceManager.Get<GameObject>("HotStone");
            if (stone == null) { return; }
            Instantiate(stone, pos, Quaternion.identity);
            for (int i = 0; i < pieces.Count; i++)
            {
                Destroy(pieces[i].gameObject);
            }
        }
    }

    public override void Heat()
    {
        // Already max hot
    }
}
