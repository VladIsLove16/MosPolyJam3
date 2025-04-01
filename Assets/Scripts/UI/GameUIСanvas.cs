using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUICanvas : MonoBehaviour
{
    [SerializeField] private DamageStatisticUICanvas damageStatistic;
    [SerializeField] private TextMeshProUGUI bulletsAmount;
    [SerializeField] private TextMeshProUGUI gameOverLabel;
    [SerializeField] private Image healthProgress;
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private Button repeatButton;
    [SerializeField] private Button home;

    private Player player;
    private HealthComponent healthComponent;
    private Weapon weapon;
    private ScoreManager scoreManager;

    protected virtual void Awake()
    {
        player = ServiceLocator.Current.Get<Player>();
        healthComponent = player.GetComponent<HealthComponent>();
        weapon = player.GetWeapon();
        scoreManager = ServiceLocator.Current.Get<ScoreManager>();

        List<DamageComponent> damageComponents = player.GetDamageApplier().GetDamageComponentstList();
        damageStatistic.Init(damageComponents);
        player.GetDamageApplier().DamageComponentUpdated += damageStatistic.OnAddDamageComponent;

        weapon.ammoChanged += OnAmmoChanged;
        bulletsAmount.text = weapon.GetDescription();

        healthComponent.healthChanged += UpdateHealthBar;
        scoreManager.OnScoreChanged += (int newScore) => score.text = newScore.ToString();

        repeatButton.onClick.AddListener(() => SceneLoader.Load(SceneLoader.Scene.EndlessMode));
        home.onClick.AddListener(() => SceneLoader.Load(SceneLoader.Scene.MainMenu));

        ServiceLocator.Current.Get<EventBus>().Subscribe<GameOver>(OnGameOver);
        ServiceLocator.Current.Get<EventBus>().Subscribe<GameWon>(OnGameWon);
    }

    private void OnGameWon(GameWon won)
    {
        ShowGameOver();
        gameOverLabel.text = $"ПОБЕДА!\nСчет: {scoreManager.Score}";
    }

    private void Start()
    {
        HideGameOver();
        UpdateHealthBar();
    }

    [ContextMenu("ShowGameOver")]
    public void ShowGameOver()
    {
        gameOverLabel.gameObject.SetActive(true);
    }

    [ContextMenu("HideGameOver")]
    public void HideGameOver()
    {
        gameOverLabel.gameObject.SetActive(false);
    }

    private void OnGameOver(GameOver over)
    {
        ShowGameOver();
        gameOverLabel.text = $"ПОРАЖЕНИЕ\nСчет: {scoreManager.Score}";
    }

    private void OnAmmoChanged()
    {
        bulletsAmount.text = weapon.GetDescription();
    }

    private void UpdateHealthBar()
    {
        float healthRatio = healthComponent.GetCurrentHealth() / healthComponent.MaxHealth;
        healthProgress.fillAmount = healthRatio;
    }
}