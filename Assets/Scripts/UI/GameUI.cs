using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameUI : MonoBehaviour
{
    DamageStatisticUI damageStatistic;
    VisualElement root;
    VisualElement bottomPanel;
    Label BulletsAmount;
    Player Player;
    HealthComponent HealthComponent;
    protected ProgressBar healthProgress;
    Weapon weapon;

    protected virtual void Start()
    {
        var uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;
        Player = ServiceLocator.Current.Get<Player>();

        List<DamageComponent> damageComponents = Player.GetDamageApplier().GetDamageComponentstList();
        damageStatistic = new(root.Q("DamageStatistic"));
        Debug.Log("damageStatistic + " + damageStatistic.IsUnityNull() + damageComponents.Count);
        damageStatistic.Init(damageComponents);
        Player.GetDamageApplier().DamageComponentUpdate += damageStatistic.OnAddDamageComponent;

        bottomPanel = root.Q("BottomPanel");

        BulletsAmount = bottomPanel.Q("BulletsAmount") as Label;
        weapon = Player.GetWeapon();
        BulletsAmount.text = weapon.GetDescription();
        weapon.ammoChanged += OnAmmoChanged;

        HealthComponent = Player.GetComponent<HealthComponent>();
        HealthComponent.healthChanged += UpdateHealthBar;
        healthProgress = bottomPanel.Q("HealthBar").Q("healthProgress") as ProgressBar;

        UpdateHealthBar();  // Инициализация здоровья при запуске
    }

    private void OnAmmoChanged()
    {
        BulletsAmount.text = weapon.GetDescription();
    }

    private void UpdateHealthBar()
    {
        float healthRatio = HealthComponent.GetCurrentHealth() / HealthComponent.maxHealth;
        healthProgress.value = healthRatio * 100;

        // Устанавливаем видимость только для healthProgress
        healthProgress.style.visibility = healthRatio == 1 ? Visibility.Hidden : Visibility.Visible;
    }
}
