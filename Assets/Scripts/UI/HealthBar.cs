using UnityEngine;
using UnityEngine.UIElements;

public class HealthBar : MonoBehaviour
{
    protected VisualElement root;
    protected ProgressBar healthProgress;
    protected HealthComponent healthComponent;
    protected virtual void Start()
    {
        var uiDocument = GetComponent<UIDocument>();  // Подключите компонент UIDocument к каждому врагу
        root = uiDocument.rootVisualElement;

        healthProgress = root.Q<ProgressBar>("healthProgress");

        healthComponent = GetComponent<HealthComponent>();
        healthComponent.healthChanged += UpdateHealthBar;
    }

    protected void Update()
    {
        UpdateHealthBarPosition();
        UpdateHealthBar();
    }

    protected void UpdateHealthBar()
    {
        if (healthComponent.GetCurrentHealth() == healthComponent.maxHealth)
        {
            root.style.visibility = Visibility.Hidden;
        }
        else
        {
            root.style.visibility = Visibility.Visible;
        }
        float healthRatio = healthComponent.GetCurrentHealth() / healthComponent.maxHealth;
        healthProgress.value = healthRatio * 100;
    }

    protected virtual void UpdateHealthBarPosition()
    {
        Vector3 worldPos = transform.position + Vector3.up * 1;  // Смещаем полоску здоровья выше врага
        Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPos);

        root.style.left = screenPos.x - 40;
        root.style.top = Screen.height - screenPos.y;  // Инвертируем Y
    }
}
