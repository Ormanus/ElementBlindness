using UnityEngine;

public class TileHotStone : TileBase
{
    public override void Freeze()
    {
        Outloud.Common.AudioManager.PlaySound(FreezingSound);
        Change(TileType.Stone);
    }

    public override void Heat()
    {
        Outloud.Common.AudioManager.PlaySound(HeatingSound);
        ChangeToLiquid(TileType.Lava);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<TileWater>(out var water))
        {
            Freeze();
            water.Heat();
        }
    }
}
