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
        Earth,
        None,
    }

    public List<Tag> Tags = new();
    public AudioClip FreezingSound;
    public AudioClip HeatingSound;

    public abstract void Heat();

    public abstract void Freeze();

    bool _destroyed = false;

    public void SetVisible(bool visible)
    {
        var renderers = GetComponentsInChildren<Renderer>();
        foreach (var renderer in renderers)
        {
            renderer.enabled = visible;
        }
    }

    protected virtual void Start()
    {
        if (Tags.Contains(MaskController.Instance.currentElement))
        {
            SetVisible(false);
        }
    }

    public void Change(TileType type)
    {
        if (_destroyed) return;
        _destroyed = true;
        SpawnTile(type, transform);
        Destroy(gameObject);
    }

    public void ChangeToLiquid(TileType type)
    {
        if (_destroyed) return;
        _destroyed = true;
        SpawnLiquid(type, transform.position);
        Destroy(gameObject);
    }

    public static void SpawnTile(TileType type, Transform tr)
    {
        var newTile = ResourceManager.Get<GameObject>(type.ToString());
        if (newTile != null)
        {
            var obj = Instantiate(newTile, tr.position, tr.rotation, null);
            obj.transform.localScale = tr.localScale;
        }
    }

    public static void SpawnLiquid(TileType type, Vector2 position)
    {
        var prefab = ResourceManager.Get<GameObject>(type.ToString());
        Vector3 pos = position;
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
    }
}
