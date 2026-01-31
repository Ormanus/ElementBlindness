using UnityEngine;

[CreateAssetMenu(fileName = "Element", menuName = "Scriptable Objects/Element")]
public class Element : ScriptableObject
{
    public string Name;
    public TileBase.Tag Type;
    public Sprite Icon;
    public Sprite PickupSprite;
    public GameObject stonePrefab;
}
