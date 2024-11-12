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
    Weapon currentWeapon;
    Inventory inventory;
    InventoryFabric inventoryFabric;
    private int weaponNum;
    HealthComponent healthComponent;
    private Animator animator;
    private PlayerMovement playerMovement;
    private void Awake()
    {
        inventory = ServiceLocator.Current.Get<Inventory>();
        inventoryFabric = ServiceLocator.Current.Get<InventoryFabric>();
        foreach (var weapon in Weapons) {
            weapon.GetEmmiter().SetDamageApplier(damageApplier);
        }
        SwitchWeaponByType(Weapons[weaponNum].GetType());
        healthComponent= GetComponent<HealthComponent>();
        healthComponent.died += OnDie;
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        if (SceneLoader.CurrentScene == SceneLoader.Scene.Level1 || SceneLoader.CurrentScene == SceneLoader.Scene.Level4)
        {
            damageApplier.electric.ResetStats();
            damageApplier.poison.ResetStats();
            damageApplier.phys.ResetStats();
        }
    }

    private void OnDie()
    {
        SoundManager.PlaySound(SoundManager.Sound.PlayerDied);
        gameObject.SetActive(false);
        ServiceLocator.Current.Get<EventBus>().Invoke(new GameOver());
        Time.timeScale = 0;
    }

    private void Update()
    {
        animator.SetBool("Moving", playerMovement.IsMoving);
    }
    internal Inventory GetInventory()
    {
        return inventory;
    }
    public DamageApplier GetDamageApplier()
    {
        return damageApplier;
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
        currentWeapon.Shoot();
    }
    public Weapon SwitchWeaponByType(Type weaponType)
    {
        foreach (Weapon weapon in Weapons)
        {
            if (weapon.GetType() == weaponType)
            {
                HideAllWeapon();
                SetWeapon(weapon);
                Debug.Log("Switched to weapon: " + currentWeapon.name);
                return weapon;
            }
        }
        Debug.Log("Weapon of type " + weaponType + " not found.");
        if(Weapons.Count==0)
            return null;
        return
            SwitchWeaponByType(Weapons[0].GetType());

    }

    private void SetWeapon(Weapon  weapon)
    {
        currentWeapon =  weapon;
        weapon.gameObject.SetActive(true);
    }
    public Weapon GetWeapon()
    {
        if(currentWeapon == null)
            SwitchWeaponByType(Weapons[weaponNum].GetType());
        return currentWeapon;
    }
    private void HideAllWeapon()
    {
        foreach (Weapon weapon in Weapons)
        {
            weapon.gameObject.SetActive(false);
        }
    }
}
