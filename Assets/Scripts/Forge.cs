using System.Collections.Generic;
using UnityEngine;

public class Forge : MonoBehaviour
{
    public static bool IsInUse = false;

    public Transform player;
    public TMPro.TextMeshPro usePrompt;
    public TMPro.TextMeshPro craftPrompt;
    public float interactionDistance = 2.0f;
    public SpriteRenderer[] stoneMaterials;

    int _currentIndex = 0;
    List<Element> _availableElements = new();


    private void Start()
    {
        usePrompt.enabled = false;
        craftPrompt.enabled = false;
        for (int i = 0; i < stoneMaterials.Length; i++)
        {
            stoneMaterials[i].enabled = false;
        }
    }

    private void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance <= interactionDistance)
        {
            usePrompt.enabled = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                IsInUse = !IsInUse;

                if (IsInUse)
                {
                    _availableElements.Clear();
                    foreach (var item in StoneThrowing.inventory)
                    {
                        // Check if player has at least 3 of the same element to unlock it
                        if (StoneThrowing.inventory.FindAll(e => e == item).Count >= 3 && !_availableElements.Contains(item))
                        {
                            _availableElements.Add(item);
                        }
                    }
                    _currentIndex = 0;
                    UpdateCraftDisplay();
                }
            }

            if (IsInUse)
            {
                usePrompt.enabled = false;

                if (_availableElements.Count == 0)
                {
                    craftPrompt.enabled = false;
                    for (int i = 0; i < stoneMaterials.Length; i++)
                    {
                        stoneMaterials[i].enabled = false;
                    }
                    return;
                }

                if (Input.GetKeyDown(KeyCode.D))
                {
                    _currentIndex = (_currentIndex + 1) % _availableElements.Count;
                    UpdateCraftDisplay();
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    _currentIndex = (_currentIndex - 1 + _availableElements.Count) % _availableElements.Count;
                    UpdateCraftDisplay();
                }
                if (Input.GetKeyDown(KeyCode.F))
                {
                    Element selectedElement = _availableElements[_currentIndex];
                    for (int i = 0; i < 3; i++)
                    {
                        StoneThrowing.inventory.Remove(selectedElement);
                    }
                    MaskController.availableMasks.Add(selectedElement);

                    _availableElements.Clear();
                    foreach (var item in StoneThrowing.inventory)
                    {
                        if (StoneThrowing.inventory.FindAll(e => e == item).Count >= 3 && !_availableElements.Contains(item))
                        {
                            _availableElements.Add(item);
                        }
                    }
                    if (_currentIndex >= _availableElements.Count)
                    {
                        _currentIndex = 0;
                    }
                    UpdateCraftDisplay();
                    StoneThrowing.Instance.UpdateIcons();
                }

            }
        }
        else
        {
            usePrompt.enabled = false;
            IsInUse = false;
        }
    }

    void UpdateCraftDisplay()
    {
        if (_availableElements.Count == 0)
        {
            foreach (var stone in stoneMaterials)
            {
                stone.enabled = false;
            }
            craftPrompt.enabled = false;
            return;
        }
        Element element = _availableElements[_currentIndex];
        for (int i = 0; i < stoneMaterials.Length; i++)
        {
            stoneMaterials[i].enabled = true;
            stoneMaterials[i].sprite = element.Icon;
        }
        craftPrompt.enabled = true;
    }

}
