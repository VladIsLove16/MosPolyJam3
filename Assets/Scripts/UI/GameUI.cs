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
    Label GameOverLabel;
    Player Player;
    HealthComponent HealthComponent;
    protected ProgressBar healthProgress;
    Weapon weapon;
    Label Score;
    ScoreManager scoreManager;
    Button repeatButton;
    Button home;
    protected virtual void Awake()
    {
        var uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;
        Player = ServiceLocator.Current.Get<Player>();

        List<DamageComponent> damageComponents = Player.GetDamageApplier().GetDamageComponentstList();
        damageStatistic = new(root.Q("DamageStatistic"));
        Debug.Log("damageStatistic + " + damageStatistic.IsUnityNull() + damageComponents.Count);
        damageStatistic.Init(damageComponents);
        Player.GetDamageApplier().DamageComponentUpdate += damageStatistic.OnAddDamageComponent;
        weapon = Player.GetWeapon();
        weapon.ammoChanged += OnAmmoChanged;

        bottomPanel = root.Q("BottomPanel");
        BulletsAmount = bottomPanel.Q("BulletsAmount") as Label;
        BulletsAmount.text = weapon.GetDescription();

        HealthComponent = Player.GetComponent<HealthComponent>();
        HealthComponent.healthChanged += UpdateHealthBar;
        healthProgress = bottomPanel.Q("HealthBar").Q("healthProgress") as ProgressBar;
        Score = root.Q<Label>("Score");
        scoreManager = ServiceLocator.Current.Get<ScoreManager>();
        scoreManager.OnScoreChanged += (int score) => Score.text = score.ToString();

        GameOverLabel = root.Q<Label>("GameOverLabel");
        repeatButton = GameOverLabel.Q<Button>("repeatButton");
        home = GameOverLabel.Q<Button>("home");
        repeatButton.clicked += () => SceneLoader.Load(SceneLoader.Scene.Level4);
        home.clicked += () => SceneLoader.Load(SceneLoader.Scene.MainMenu);
        ServiceLocator.Current.Get<EventBus>().Subscribe<GameOver>(OnGameOver);
        ServiceLocator.Current.Get<EventBus>().Subscribe<GameWon>(OnGameWon);
    }

    private void OnGameWon(GameWon won)
    {
        ShowGameOver();
        GameOverLabel.text = $"ПОБЕДА!\nCчет: {scoreManager.Score.ToString()}";
    }

    private void Start()
    {
        HideGameOver();
        UpdateHealthBar();  // Инициализация здоровья при запуске
    }
    [ContextMenu("ShowGameOver")]
    public void ShowGameOver()
    {
        GameOverLabel.SetEnabled(true);
        GameOverLabel.style.visibility = Visibility.Visible;
    }
    [ContextMenu("HideGameOver")]
    public void HideGameOver()
    {
        GameOverLabel.SetEnabled(false);
        GameOverLabel.style.visibility = Visibility.Hidden;
    }
    private void OnGameOver(GameOver over)
    {
        ShowGameOver();
        GameOverLabel.text = $"ПОРАЖЕНИЕ\nCчет: {scoreManager.Score.ToString()}";
    }
    private void OnAmmoChanged()
    {
        BulletsAmount.text = weapon.GetDescription();
    }

    private void UpdateHealthBar()
    {
        float healthRatio = HealthComponent.GetCurrentHealth() / HealthComponent.MaxHealth;
        healthProgress.value = healthRatio * 100;
    }
}
