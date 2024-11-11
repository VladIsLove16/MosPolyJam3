
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CustomAnimationReplacer : MonoBehaviour
{
    public AnimationClip animationClip; // Общая анимация
    public Sprite replacementSprite; // Один спрайт для замены каждого второго ключевого кадра

    private void Start()
    {
        ApplyCustomSprite();
    }

    [ContextMenu("Apply Custom Sprite")]
    public void ApplyCustomSprite()
    {
        if (animationClip == null || replacementSprite == null)
        {
            Debug.LogError("Animation Clip or Replacement Sprite is not assigned.");
            return;
        }

        // Получаем все кривые анимации для `SpriteRenderer.sprite`
        EditorCurveBinding[] spriteBindings = AnimationUtility.GetObjectReferenceCurveBindings(animationClip);

        foreach (var binding in spriteBindings)
        {
            if (binding.propertyName == "m_Sprite") // Убедимся, что это ключевые кадры для спрайта
            {
                // Получаем текущие ключевые кадры
                ObjectReferenceKeyframe[] keyframes = AnimationUtility.GetObjectReferenceCurve(animationClip, binding);

                // Заменяем каждый второй ключевой кадр на новый спрайт
                for (int i = 1; i < keyframes.Length; i += 2)
                {
                    keyframes[i].value = replacementSprite;
                }

                // Сохраняем обновленные ключевые кадры в клипе
                AnimationUtility.SetObjectReferenceCurve(animationClip, binding, keyframes);
            }
        }

        Debug.Log("Every second keyframe replaced with the custom sprite.");
    }
}
