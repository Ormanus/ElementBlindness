using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaskController : MonoBehaviour
{
    public static MaskController Instance;

    public static List<Element> availableMasks;

    public TileBase.Tag currentElement;
    public Image colorOverlay;

    const int layerPlayer = 7;
    const int layerHot = 8;
    const int layerCold = 9;

    private void Awake()
    {
        Instance = this;
    }

    public void ApplyMask(Element element)
    {
        currentElement = element.Type;
        colorOverlay.color = element.overlayColor;
        Physics2D.IgnoreLayerCollision(layerPlayer, layerHot, element.Type == TileBase.Tag.Hot);
        Physics2D.IgnoreLayerCollision(layerPlayer, layerCold, element.Type == TileBase.Tag.Cold);

        var all = FindObjectsByType<TileBase>(FindObjectsSortMode.None);
        foreach (var tile in all)
        {
            bool visible = !tile.Tags.Contains(element.Type);
            tile.SetVisible(visible);
        }
    }
}
