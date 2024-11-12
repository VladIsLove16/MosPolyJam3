using System;
using UnityEngine;

public class InventoryItem : SpawnObject
{
    private bool used = false;  
    public  InventoryItemType inventoryItemType;
    [SerializeField]
    SoundManager.Sound UseSounds;
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if(player !=null)
        {
            if(used)
                return;
            Inventory inventory = player.GetInventory();
            inventory.AddItem(inventoryItemType);
            Taken?.Invoke();
            Destroy(gameObject);
        }
    }
    public virtual void Use()
    {
        SoundManager.PlaySound(UseSounds);
        Debug.Log("using" + inventoryItemType);
        used = true;
    }
}
