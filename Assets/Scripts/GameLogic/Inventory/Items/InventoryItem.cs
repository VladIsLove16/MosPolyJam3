using System;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    private bool used = false;  
    public  InventoryItemType inventoryItemType;
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if(player !=null)
        {
            if(used)
                return;
            Inventory inventory = player.GetInventory();
            inventory.AddItem(inventoryItemType);
            Destroy(gameObject);
        }
    }
    public virtual void Use()
    {
        Debug.Log("using" + inventoryItemType);
        used = true;
    }
}
