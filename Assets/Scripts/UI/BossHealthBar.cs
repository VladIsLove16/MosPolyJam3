using UnityEngine.UIElements;

public class BossHealthBar : HealthBar
{
    protected override void UpdateHealthBarPosition()
    {
        //Vector3 worldPos = transform.position + Vector3.up * 2;  // Смещаем полоску здоровья выше врага
        //Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPos);

        //root.style.left = screenPos.x;
        //root.style.top = Screen.height - screenPos.y;  // Инвертируем Y
        // Устанавливаем полоску здоровья в центр экрана по горизонтали и немного ниже верхнего края по вертикали
        root.style.left = new Length(50, LengthUnit.Percent);  // Центр по горизонтали
        root.style.top = new Length(5, LengthUnit.Percent);    // Отступ от верхнего края
    }
}
