using System.Collections.Generic;
using UnityEngine;

public abstract class TileBase : MonoBehaviour
{
    public enum TileType
    {
        Stone,
        Wood,
        Ice,
        Water,
        Lava,
        ColdStone,
        ColdWood,
        HotStone,
        BurningWood,
    }

    public enum Tag
    {
        Hot,
        Cold,
        Water,
    }

    public List<Tag> Tags = new();

    public abstract void Heat();

    public abstract void Freeze();

    public void Change(TileType type)
    {
        var newTile = ResourceManager.Get<GameObject>(type.ToString());
        if (newTile != null)
            Instantiate(newTile, transform.position, transform.rotation, null);
        Destroy(gameObject);
    }

    public void ChangeToLiquid(TileType type)
    {
        var prefab = ResourceManager.Get<GameObject>(type.ToString());
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
