
using UnityEditor;
using UnityEngine;

public class KeyframeReplacer : MonoBehaviour
{
    public AnimationClip animationClip; // Анимационный клип, где мы будем заменять ключевые кадры
    public Sprite laggingSprite; // Спрайт, который будет заменять каждый второй ключевой кадр
    public Sprite originalSprite; // Спрайт, который будет заменять каждый второй ключевой кадр

    [ContextMenu("Replace Every Second Keyframe")]
    public void ReplaceEverySecondKeyframe()
    {
        if (animationClip == null || originalSprite == null|| laggingSprite == null)
        {
            Debug.LogError("Animation Clip or New Sprite is not assigned.");
            return;
        }

        EditorCurveBinding[] spriteBindings = AnimationUtility.GetObjectReferenceCurveBindings(animationClip);

        foreach (var binding in spriteBindings)
        {
            if (binding.propertyName == "m_Sprite") // Убедимся, что это ключевые кадры для спрайта
            {
                // Получаем текущие ключевые кадры
                ObjectReferenceKeyframe[] keyframes = AnimationUtility.GetObjectReferenceCurve(animationClip, binding);

                // Меняем каждый второй ключевой кадр на новый спрайт
                for (int i = 1; i < keyframes.Length; i += 2)
                {
                    keyframes[i].value = laggingSprite;
                }
                // Меняем каждый второй ключевой кадр на новый спрайт
                for (int i = 0; i < keyframes.Length; i += 2)
                {
                    keyframes[i].value = originalSprite;
                }

                // Сохраняем обновленные ключевые кадры в клипе
                AnimationUtility.SetObjectReferenceCurve(animationClip, binding, keyframes);
            }
        }

    }
}
