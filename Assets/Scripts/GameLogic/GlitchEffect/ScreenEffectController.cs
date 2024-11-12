using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ScreenEffectController : MonoBehaviour
{
    public GlitchEffect glitchEffect;  // Ссылка на компонент GlitchEffect
    public ScreenDistortionEffect distortionEffect;  // Ссылка на компонент ScreenDistortionEffect

    public float glitchAmount = 0.2f; // Интенсивность глюка
    public float glitchDuration = 0.5f; // Длительность глюка
    public float timeBetweenGlitches = 2f; // Время между "волнами" глюков

    public float distortionStrength = 0.1f; // Сила искажения
    public float distortionSpeed = 1.0f; // Скорость искажения

    private float glitchTimer = 0f;
    private float glitchCooldown = 0f;
    [SerializeField]
    LocationLagger LocationLagger;
    [SerializeField]
    float delay = 0.3f;
    private void Start()
    {
        if (glitchEffect != null)
        {
            glitchEffect.glitchMaterial.SetFloat("_GlitchAmount", 0f); // Отключаем эффект глюка по умолчанию
        }
        if (distortionEffect != null)
        {
            distortionEffect.distortionMaterial.SetFloat("_DistortionStrength", 0f); // Отключаем искажение по умолчанию
        }
    }

    private void Update()
    {
        // Управление глюком
        glitchCooldown -= Time.deltaTime;
        if (glitchCooldown <= 0f)
        {
            glitchCooldown = timeBetweenGlitches;
            glitchTimer = glitchDuration;

            // Активируем глюк
            if (glitchEffect != null && distortionEffect != null)
            {
                SoundManager.PlaySound(SoundManager.Sound.DistortionSound);
                glitchEffect.glitchMaterial.SetFloat("_GlitchAmount", glitchAmount);
                distortionEffect.distortionMaterial.SetFloat("_DistortionStrength", distortionStrength);
                LocationLagger.StartLags(delay);
            }
        }

        // Отключаем глюк по завершении
        if (glitchTimer > 0f)
        {
            glitchTimer -= Time.deltaTime;
        }
        else
        {
            if (glitchEffect != null)
            {
                glitchEffect.glitchMaterial.SetFloat("_GlitchAmount", 0f);
            }
            if(distortionEffect != null)
            {

                distortionEffect.distortionMaterial.SetFloat("_DistortionStrength", 0);
            }
        }
    }

    // Метод для включения/отключения эффектов
    public void ToggleEffects(bool isActive)
    {
        if (glitchEffect != null)
        {
            glitchEffect.enabled = isActive;
        }
        if (distortionEffect != null)
        {
            distortionEffect.enabled = isActive;
        }
    }

    // Метод для обновления параметров эффектов
    public void SetEffectParameters(float newGlitchAmount, float newDistortionStrength, float newDistortionSpeed)
    {
        glitchAmount = newGlitchAmount;
        distortionStrength = newDistortionStrength;
        distortionSpeed = newDistortionSpeed;
    }
}
