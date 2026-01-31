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
        FrozenStone,
        FrozenWood,
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
}
