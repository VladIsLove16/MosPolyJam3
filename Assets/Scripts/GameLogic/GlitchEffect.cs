using UnityEngine;

[RequireComponent(typeof(Camera))]
public class GlitchEffect : MonoBehaviour
{
    public Material glitchMaterial;  // Материал с шейдером
    public float glitchAmount = 0.2f; // Вероятность появления битых пикселей
    public float glitchDuration = 0.5f; // Длительность глюков (время "волны")
    public float timeBetweenGlitches = 2f; // Время между "волнами" глюков

    private float glitchTimer = 0f;
    private float glitchCooldown = 0f;

    private void Start()
    {
        if (glitchMaterial != null)
        {
            glitchMaterial.SetFloat("_GlitchAmount", 0f); // Изначально глюк выключен
        }
    }

    private void Update()
    {
        glitchCooldown -= Time.deltaTime;
        if (glitchCooldown <= 0f)
        {
            // Запускаем новый "глюк"
            glitchCooldown = timeBetweenGlitches;
            glitchTimer = glitchDuration;

            // Активируем глюк
            glitchMaterial.SetFloat("_GlitchAmount", glitchAmount);
        }

        // Управляем длительностью эффекта глюка
        if (glitchTimer > 0f)
        {
            glitchTimer -= Time.deltaTime;
        }
        else
        {
            // Когда глюк заканчивается, сбрасываем
            glitchMaterial.SetFloat("_GlitchAmount", 0f);
        }
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (glitchMaterial != null)
        {
            Graphics.Blit(source, destination, glitchMaterial);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }
}
