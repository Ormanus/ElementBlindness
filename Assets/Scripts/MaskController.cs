using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaskController : MonoBehaviour
{
    public static MaskController Instance;

    public static List<Element> availableMasks;

    public TileBase.Tag currentElement;
    public Image colorOverlay;
    public Transform animMask;
    public GameObject maskIconPrefab;
    public Transform masksParent;
    public AudioClip maskSound;

    const int layerPlayer = 2; // Ignore Raycast
    const int layerHot = 8;
    const int layerCold = 9;

    float outOfScreenY = -8f;
    int _index = 0;
    List<GameObject> _maskIcons = new();

    private void Awake()
    {
        Instance = this;
        currentElement = TileBase.Tag.None;
        availableMasks = new();
        availableMasks.Add(null); // No mask
        animMask.gameObject.SetActive(false);

        UpdateMaskSelection();
    }

    private void Start()
    {
        // Ensure physics settings are correct at start
        UpdateMaskEffect();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            _index = (_index + 1) % availableMasks.Count;
            UpdateMaskSelection();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Element selectedMask = availableMasks[_index];
            ApplyMask(selectedMask);
        }
    }

    public static void AddMask(Element element)
    {
        if (!availableMasks.Contains(element))
        {
            availableMasks.Add(element);
            Instance.UpdateMaskSelection();
        }
    }

    public void ApplyMask(Element element)
    {
        if (element == null)
        {
            currentElement = TileBase.Tag.None;
            StartCoroutine(MaskOffAnim());
        }
        else
        {
            Outloud.Common.AudioManager.PlaySound(maskSound);
            StartCoroutine(MaskOnAnim(element));
        }
    }

    void UpdateMaskEffect()
    {
        Physics2D.IgnoreLayerCollision(layerPlayer, layerHot, currentElement == TileBase.Tag.Hot);
        Physics2D.IgnoreLayerCollision(layerPlayer, layerCold, currentElement == TileBase.Tag.Cold);

        var all = FindObjectsByType<TileBase>(FindObjectsSortMode.None);
        foreach (var tile in all)
        {
            bool visible = !tile.Tags.Contains(currentElement);
            tile.SetVisible(visible);
        }
    }

    public void UpdateMaskSelection()
    {
        _index %= availableMasks.Count;
        foreach (var icon in _maskIcons)
        {
            Destroy(icon);
        }
        _maskIcons.Clear();
        for (int i = 0; i < availableMasks.Count; i++)
        {
            var icon = Instantiate(maskIconPrefab, masksParent);
            float size = i == 0 ? 80 : 50; // Highlight the selected mask
            icon.GetComponent<RectTransform>().sizeDelta = new Vector2(size, size);
            int index = (i + _index) % availableMasks.Count;
            if (availableMasks[index] == null)
            {
                icon.GetComponent<Image>().color = Color.white;
            }
            else
            {
                Color maskColor = availableMasks[index].overlayColor;
                maskColor.a = 1f;
                icon.GetComponent<Image>().color = maskColor;
            }
            _maskIcons.Add(icon);
        }
    }

    IEnumerator MaskOnAnim(Element element)
    {
        if (currentElement != TileBase.Tag.None)
        {
            yield return StartCoroutine(MaskOffAnim());
        }

        currentElement = element.Type;

        Vector3 startpos = Camera.main.transform.position + Vector3.up * outOfScreenY;
        Vector3 endpos = Camera.main.transform.position;
        startpos.z = endpos.z = 0f;

        Color startColor = element.overlayColor;
        Color endColor = element.overlayColor;
        endColor.a = 0f;

        SpriteRenderer sr = animMask.GetComponent<SpriteRenderer>();

        colorOverlay.color = endColor;

        animMask.gameObject.SetActive(true);
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 1.5f;
            animMask.position = Vector3.Lerp(startpos, endpos, Easing.EaseOut(t));

            colorOverlay.color = Color.Lerp(endColor, startColor, Easing.EaseOut(t));
            sr.color = Color.Lerp(startColor, endColor, Easing.EaseOut(t));
            yield return null;
        }
        UpdateMaskEffect();

        animMask.gameObject.SetActive(false);
    }

    IEnumerator MaskOffAnim()
    {
        Vector3 startpos = Camera.main.transform.position;
        Vector3 endpos = Camera.main.transform.position + Vector3.up * outOfScreenY;
        startpos.z = endpos.z = 0f;

        Color startColor = colorOverlay.color;
        Color endColor = colorOverlay.color;
        Color overlayStart = colorOverlay.color;

        endColor.a = 1f;
        startColor.a = 0f;

        SpriteRenderer sr = animMask.GetComponent<SpriteRenderer>();

        animMask.gameObject.SetActive(true);
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 2f;
            animMask.position = Vector3.Lerp(startpos, endpos, Easing.EaseOut(t));
            colorOverlay.color = Color.Lerp(overlayStart, startColor, Easing.EaseOut(t));
            sr.color = Color.Lerp(startColor, endColor, Easing.EaseOut(t));
            yield return null;
        }
        currentElement = TileBase.Tag.None;
        UpdateMaskEffect();
        animMask.gameObject.SetActive(false);
    }
}
