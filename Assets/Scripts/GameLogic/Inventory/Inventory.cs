using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : IService
{
    InventoryFabric InventoryFabric;
    // Событие для обновления UI при из
    // Invменении количества предметов
    public Inventory()
    {
        items = new Dictionary<InventoryItemType, int>()
        {
            [InventoryItemType.cottons] = 0,
            [InventoryItemType.rat] = 1,
            [InventoryItemType.milk] = 2,
        };
    }
    public event Action<InventoryItemType, int> OnItemAmountChanged;

    private Dictionary<InventoryItemType, int> items = new();

    public void AddItem(InventoryItemType itemType, int amount = 1)
    {
        Debug.Log("Added" +  itemType);
        if (items.ContainsKey(itemType))
            items[itemType] += amount;
        else
            items[itemType] = amount;

        OnItemAmountChanged?.Invoke(itemType, items[itemType]);
    }

    public bool RemoveItem(InventoryItemType itemType, int amount = 1)
    {
        Debug.Log("Removed" + itemType);
        if (items.ContainsKey(itemType) && items[itemType] > 0)
        {
            items[itemType]--;
            OnItemAmountChanged?.Invoke(itemType, items[itemType]);
            Debug.Log("OnItemAmountChanged" + items[itemType]);
            return true;
        }
        else
            return false;
    }

    public Dictionary<InventoryItemType, int> GetItems() => new(items);

    internal bool Contains(InventoryItemType inventoryitemType)
    {
        return items.ContainsKey(inventoryitemType) && items[inventoryitemType]>0;
    }
    public int GetItemAmount(InventoryItemType itemType)
    {
        return items[itemType];
    }
}
