using UnityEngine;

public class TileWoodBurning : TileBase
{
    float _startTime;
    float _timer;

    public override void Freeze()
    {
        Change(TileType.Wood);
    }

    public override void Heat()
    {
        // Max hot
    }

    protected override void Start()
    {
        base.Start();
        _startTime = Time.time;
        _timer = 3f + Random.value;
    }

    private void Update()
    {
        if (Time.time > _startTime + _timer)
        {
            Destroy(gameObject);
        }
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
