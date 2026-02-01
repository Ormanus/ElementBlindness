using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileWater : TileLiquid
{
    private void FixedUpdate()
    {
        var rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Random.insideUnitCircle * 2f);
    }

    public override void Freeze()
    {
        Outloud.Common.AudioManager.PlaySound(FreezingSound);
        FreezeLiquid<TileWater>(TileType.Ice);
    }

    public override void Heat()
    {
        Outloud.Common.AudioManager.PlaySound(HeatingSound);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<TileLava>(out var lava))
        {
            Heat();
        }
    }
}
