using System.Security.Cryptography;
using UnityEngine;

public class LocationLag : MonoBehaviour
{
    private Animator animator;
    float animationTime;
    private void Awake()
    {
        animator = GetComponent<Animator>();

        AnimationClip clip = animator.runtimeAnimatorController.animationClips[1];
        animationTime = clip.length;
        Debug.Log("Duration of the animation: " + animationTime);
    }

    private void Update()
    {
        // Получаем информацию о текущем состоянии анимации
        

         // Пример задержки перед сбросом триггера
    }   

    private void ResetTrigger()
    {
        Debug.Log("ResetTrigger(Lags)");
        animator.ResetTrigger("Lags");
    }
    public void StartLags()
    {
        Debug.Log("SetTrigger(Lags)");
        animator.SetTrigger("Lags");
        Invoke(nameof(ResetTrigger), animationTime);
    }
}
