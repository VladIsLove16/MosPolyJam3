using System;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour , IService
{
    [SerializeField]
    private DamageApplier damageApplier;
    private Emitter emitter;
    [SerializeField]
    private List<Weapon> Weapons;
    private Weapon CurrentWeapon;
    public DamageApplier GetDamageApplier() { return damageApplier; }
    Inventory inventory;
    InventoryFabric inventoryFabric;
    private int weaponNum;
    HealthComponent healthComponent;
    private void Awake()
    {
        inventory = ServiceLocator.Current.Get<Inventory>();
        inventoryFabric = ServiceLocator.Current.Get<InventoryFabric>();
        foreach (var weapon in Weapons) {
            weapon.GetEmmiter().SetDamageApplier(damageApplier);
        }
        SwitchWeaponByType(Weapons[weaponNum].GetType());
        healthComponent= GetComponent<HealthComponent>();
    }

    internal Inventory GetInventory()
    {
        return inventory;
    }
    public void Use(InventoryItemType inventoryitemType)
    {
        if (inventory.RemoveItem(inventoryitemType))
        {
            InventoryItem item = inventoryFabric.Create(inventoryitemType);
            item.Use();
        }
        else
            Debug.Log("Not enough items of " + inventoryitemType);
    }
    public void Shoot()
    {
        CurrentWeapon.Shoot();
    }
    public void SwitchWeaponByType(Type weaponType)
    {
        foreach (Weapon weapon in Weapons)
        {
            if (weapon.GetType() == weaponType)
            {
                HideAllWeapon();
                SetWeapon(weapon);
                Debug.Log("Switched to weapon: " + CurrentWeapon.name);
                return;
            }
        }
        Debug.Log("Weapon of type " + weaponType + " not found.");
        SwitchWeaponByType(Weapons[0].GetType());

    }

    private void SetWeapon(Weapon  weapon)
    {
        CurrentWeapon =  weapon;
        weapon.gameObject.SetActive(true);
    }
    public Weapon GetWeapon()
    {
        return CurrentWeapon;
    }
    private void HideAllWeapon()
    {
        foreach (Weapon weapon in Weapons)
        {
            weapon.gameObject.SetActive(false);
        }
    }
}
