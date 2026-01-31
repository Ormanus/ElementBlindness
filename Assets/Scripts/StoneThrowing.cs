using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoneThrowing : MonoBehaviour
{
    public static List<ElementStoneBase> inventory = new();

    public Element[] stones;
    public Image icon;

    int _index = 0;

    private void Start()
    {
        UpdateIcon();
    }

    void UpdateIcon()
    {
        icon.sprite = stones[_index].Icon;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _index = (_index + 1) % stones.Length;
            UpdateIcon();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0;

            Instantiate(stones[_index].stonePrefab, worldPos, Quaternion.identity);
        }
    }
}
