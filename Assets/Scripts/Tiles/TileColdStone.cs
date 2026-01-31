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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<TileLava>(out var lava))
        {
            Heat();
        }
    }
}
