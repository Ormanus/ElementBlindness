using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoneThrowing : MonoBehaviour
{
    public static StoneThrowing Instance;

    public static List<Element> inventory = new();

    public Element[] stones;
    public LineRenderer aimLine;
    public GameObject stoneIconPrefab;
    public RectTransform inventoryParent;

    int _index = 0;
    bool _aiming = false;
    Vector3 _aimPosition;
    List<GameObject> _stoneIcons = new();

    private void Start()
    {
        Instance = this;
        aimLine.positionCount = 15;
        aimLine.enabled = false;
        inventory.AddRange(stones);
        UpdateIcons();
    }

    public void UpdateIcons()
    {
        if (inventory.Count == 0)
            _index = 0;
        else
            _index %= inventory.Count;

        foreach (var icon in _stoneIcons)
        {
            Destroy(icon);
        }
        _stoneIcons.Clear();
        for (int i = 0; i < inventory.Count; i++)
        {
            var icon = Instantiate(stoneIconPrefab, inventoryParent);
            float size = i == 0 ? 60 : 50; // Highlight the selected stone
            icon.GetComponent<RectTransform>().sizeDelta = new Vector2(size, size);
            int index = (i + _index) % inventory.Count;
            icon.GetComponent<Image>().sprite = inventory[index].Icon;
            _stoneIcons.Add(icon);
        }
    }

    public static void AddStone(Element element)
    {
        inventory.Add(element);
        Instance.UpdateIcons();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _index = (_index + 1) % stones.Length;
            UpdateIcons();
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (inventory.Count == 0)
                return;
            _aiming = true;

            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0;
        }
        if (_aiming)
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0;
            _aimPosition = worldPos;
            Vector3 direction = (_aimPosition - transform.position);
            aimLine.enabled = true;
            for (int i = 0; i < aimLine.positionCount; i++)
            {
                float t = i / (float)(aimLine.positionCount - 1);
                float gravity = Physics2D.gravity.y;
                float speed = direction.magnitude * 2f;
                Vector3 point = transform.position + direction.normalized * speed * t + 0.5f * new Vector3(0, gravity, 0) * t * t;
                aimLine.SetPosition(i, point);
            }
        }
        if (Input.GetMouseButtonDown(1) && _aiming)
        {
            _aiming = false;
            aimLine.enabled = false;
        }
        if (Input.GetMouseButtonUp(0) && _aiming)
        {
            var stone = Instantiate(inventory[_index].stonePrefab, transform.position, Quaternion.identity);
            Vector3 direction = (_aimPosition - transform.position);
            stone.GetComponent<Rigidbody2D>().linearVelocity = direction * 2f;
            _aiming = false;
            aimLine.enabled = false;
            inventory.RemoveAt(_index);
            UpdateIcons();
        }
    }
}
