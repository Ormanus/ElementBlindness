using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    public GameObject[] gameObjects;

    Dictionary<string, Object> _objs = new();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            DestroyImmediate(gameObject);
        foreach (var item in gameObjects)
        {
            _objs.Add(item.name, item);
        }
    }

    public static T Get<T>(string name) where T : Object
    {
        foreach (var kvp in Instance._objs)
        {
            if (kvp.Key == name && kvp.Value is T obj)
            {
                return obj;
            }
        }
        Debug.LogWarning("Invalid key: " + name);
        return null;
    }
}
