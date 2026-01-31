using UnityEngine;

public class TileHotStone : TileBase
{
    public override void Freeze()
    {
        Change(TileType.Stone);
    }

    public override void Heat()
    {
        var prefab = ResourceManager.Get<GameObject>("Lava");
        Vector3 pos = transform.position;
        pos += new Vector3(-0.5f, -0.5f);
        const int pieces = 4; // per dimension
        float step = 1f / pieces;
        Vector3 offset = new Vector3(step, step, 0f) * 0.5f;

        for (int i = 0; i < pieces; i++)
        {
            for (int j = 0; j < pieces; j++)
            {
                var obj = Instantiate(prefab);
                obj.transform.position = pos + offset + new Vector3(i * step, j * step, 0f);
            }
        }
        Destroy(gameObject);
    }
}
