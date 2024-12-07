using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIController : MonoBehaviour
{
    [SerializeField] private InventoryItemTemplate _inventoryItemTemplate;
    [SerializeField] private RectTransform _inventoryItemContainer;
    private List<InventoryItemTemplate> templates;
    private Stack<InventoryItemTemplate> bin;

    [SerializeField] private CanvasGroup _canvasGroupInventory;
    [SerializeField] private CanvasGroup _canvasGroupGameplay;

    private void Start()
    {
        templates = new List<InventoryItemTemplate>();
        bin = new Stack<InventoryItemTemplate>();
        CloseInventory();
    }

    public void OpenInventory()
    {
        SetVisibilityInventoryUI(true);
        SetVisibilityGameplayUI(false);
    }

    public void CloseInventory()
    {
        SetVisibilityGameplayUI(true);
        SetVisibilityInventoryUI(false);
        DestroyTemplates();
    }

    public void SetVisibilityInventoryUI(bool isOpen)
    {
        _canvasGroupInventory.alpha = isOpen ? 1 : 0;
        _canvasGroupInventory.interactable = isOpen;
        _canvasGroupInventory.blocksRaycasts = isOpen;
    }

    public void SetVisibilityGameplayUI(bool isOpen)
    {
        _canvasGroupGameplay.alpha = isOpen ? 1 : 0;
        _canvasGroupGameplay.interactable = isOpen;
        _canvasGroupGameplay.blocksRaycasts = isOpen;
    }

    public void SetupInventory(Dictionary<InGameMaterialSO, int> inventory)
    {
        foreach (var item in inventory)
        {
            InventoryItemTemplate inventoryItemUI;
            if (bin.Count > 0)
                inventoryItemUI = bin.Pop();
            else
                inventoryItemUI = Instantiate(_inventoryItemTemplate, Vector3.zero, Quaternion.identity, _inventoryItemContainer);

            inventoryItemUI.Initialize(item.Key.sprite, $"{item.Key.name} : {item.Value}");
            inventoryItemUI.gameObject.SetActive(true);
            templates.Add(inventoryItemUI);
        }
    }

    private void DestroyTemplates()
    {
        for (int i = 0; i < templates.Count; i++)
        {
            templates[i].gameObject.SetActive(false);
            bin.Push(templates[i]);
        }
        templates.Clear();
    }
}