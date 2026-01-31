using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileLava : TileLiquid
{
    private void FixedUpdate()
    {
        var rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Random.insideUnitCircle * 2f);
    }

    public override void Freeze()
    {
        FreezeLiquid<TileLava>(TileType.HotStone);
    }

    public override void Heat()
    {
        // Already max hot
    }
}
