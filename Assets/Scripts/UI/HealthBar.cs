using UnityEngine;
using UnityEngine.UIElements;
[RequireComponent(typeof(UIDocument))]
public class HealthBar : MonoBehaviour
{
    protected VisualElement root;
    protected ProgressBar healthProgress;
    VisualElement healthProgressBar;
    [SerializeField] protected HealthComponent healthComponent;
    float width;
    [SerializeField]
    float offset;
    [SerializeField]
    float upper;
    protected virtual void Awake()
    {
        var uiDocument = GetComponent<UIDocument>();  // Подключите компонент UIDocument к каждому врагу
        root = uiDocument.rootVisualElement;

        healthProgress = root.Q<ProgressBar>("healthProgress");
        healthProgressBar = healthProgress.Q<VisualElement>("unity-progress-bar");

        healthComponent.healthChanged += () => { UpdateHealthBarProgress(); };
        Debug.Log("HealthBar" + name + "created");
    }

    protected void Update()
    {
        UpdateHealthBarPosition();
        UpdateHealthBarVisibility();
        UpdateHealthBarProgress();
        UpdatehealthBarWidth();

    }
    public void Show()
    {
        root.style.visibility = Visibility.Visible;
    }
    public void Hide()
    {
        root.style.visibility = Visibility.Hidden;
    }
    protected void UpdateHealthBarProgress()
    {
        float healthRatio = healthComponent.GetCurrentHealth() / healthComponent.MaxHealth;
        healthProgress.value = healthRatio * 100;
    }

    protected virtual void UpdatehealthBarWidth()
    {
        healthProgressBar.style.width = width;
        healthProgressBar.style.maxWidth = width;
    }

    protected virtual void UpdateHealthBarVisibility()
    {
        if (healthComponent.GetCurrentHealth() == healthComponent.MaxHealth)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    protected virtual void UpdateHealthBarPosition()
    {
        Vector3 worldPos = transform.position + Vector3.up * upper;  // Смещаем полоску здоровья выше врага
        Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPos);

        root.style.left = screenPos.x - offset;
        root.style.top = Screen.height - screenPos.y;  // Инвертируем Y
    }
}