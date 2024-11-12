using UnityEngine.UIElements;

public class PlayerHealthBar : HealthBar
{
    protected override void Awake ()
    {
        base.Awake();
        healthProgress.style.maxWidth = 320;
        healthProgress.style.width = 320;
    }
    protected override void UpdateHealthBarPosition()
    {
    }
}
