using UnityEngine;

public class TileStone : TileBase
{
    public override void Freeze()
    {
        Outloud.Common.AudioManager.PlaySound(FreezingSound);
        Change(TileType.ColdStone);
    }

    public override void Heat()
    {
        Outloud.Common.AudioManager.PlaySound(HeatingSound);
        Change(TileType.HotStone);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<TileLava>(out var lava))
        {
            Heat();
        }
    }
}
