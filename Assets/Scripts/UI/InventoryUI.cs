using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
[System.Serializable]
public class InventoryItemIcon
{
    public Sprite Sprite;
    public InventoryItemType Type;
}
public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    public List<InventoryItemIcon> icons;
    private Inventory inventory;
    private Dictionary<InventoryItemType, Label> itemLabels = new();
    private Dictionary<InventoryItemType, Sprite> itemIcons = new();
    private VisualElement root;  // Ссылка на корневой элемент UI (можно установить в инспекторе)

    private void Awake()
    {
        inventory = ServiceLocator.Current.Get<Inventory>();
        inventory.OnItemAmountChanged += UpdateItemLabel;

        // Инициализация UI для каждого типа предметов
        CreateIconToItemDict();
        root = GetComponent<UIDocument>().rootVisualElement;

        int index = 0;
        VisualElement InventoryListParent = root.Q("Background").Q("InventoryListParent");
        foreach (InventoryItemType itemType in Enum.GetValues(typeof(InventoryItemType)))
        {
            VisualElement inventoryItem = GetInventoryItem(InventoryListParent, index);
            SetIcon(inventoryItem, itemType);
            Label label = inventoryItem.Q<Label>();
            itemLabels[itemType] = label;
            UpdateItemLabel(itemType, inventory.GetItemAmount(itemType));
            index++;
        }

    }

    private void CreateIconToItemDict()
    {
        foreach (InventoryItemIcon InventoryItemIcon in icons)
        {
            itemIcons.Add(InventoryItemIcon.Type, InventoryItemIcon.Sprite);
        }
    }

    private void SetIcon(VisualElement inventoryItem, InventoryItemType inventoryItemType)
    {
        VisualElement visualElement = inventoryItem.Q("Icon");
        visualElement.style.backgroundImage = new StyleBackground(itemIcons[inventoryItemType]);
    }

    private void UpdateItemLabel(InventoryItemType itemType, int newAmount)
    {
        Debug.Log("UpdateItemLabel");
        if (itemLabels.TryGetValue(itemType, out Label label))
        {
            label.text = newAmount.ToString();
        }
    }
    private VisualElement GetInventoryItem(VisualElement inventoryListParent, int index)
    {
       VisualElement inventoryItem = inventoryListParent.Children().ToList()[index];
        return inventoryItem;
    }
}
