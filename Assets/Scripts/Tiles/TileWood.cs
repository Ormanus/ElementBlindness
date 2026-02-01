using UnityEngine;

public class TileWood : TileBase
{
    public override void Freeze()
    {
        Outloud.Common.AudioManager.PlaySound(FreezingSound);
        Change(TileType.ColdWood);
    }

    public override void Heat()
    {
        Outloud.Common.AudioManager.PlaySound(HeatingSound);
        Change(TileType.BurningWood);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<TileLava>(out var lava))
        {
            Heat();
        }
    }
}
