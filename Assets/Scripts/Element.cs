using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "Element", menuName = "Scriptable Objects/Element")]
public class Element : ScriptableObject
{
    public enum ElementType
    {
        Water,
        Fire,
        Earth
    }

    public string Name;
    public ElementType Type;
    public Image Thumbnail;
    public GameObject stonePrefab;
}
