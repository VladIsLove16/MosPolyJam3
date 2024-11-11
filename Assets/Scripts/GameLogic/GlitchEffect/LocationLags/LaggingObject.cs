using UnityEngine;

public class LaggingObject : MonoBehaviour
{
    [SerializeField]
    private Sprite laggingSprite;
    [SerializeField]
    private Sprite originalSprite;
    private SpriteRenderer SpriteRenderer;
    Animator animator;
    float animationTime;
    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        animator= GetComponent<Animator>();

        AnimationClip clip = animator.runtimeAnimatorController.animationClips[1];
        animationTime = clip.length;
        Debug.Log("Duration of the animation: " + animationTime);
    }
    public void StartAnimation()
    {
        animator.SetTrigger("Lags");
    }
    public void ResetTrigger()
    {
        animator.ResetTrigger("Lags");
    }
    public void StartLags()
    {
        Debug.Log("SetTrigger(Lags)");
    }
    public void Lag()
    {
        SpriteRenderer.sprite = laggingSprite;
    }
    public void ReturnNormal()
    {
        SpriteRenderer.sprite = originalSprite;
    }
}
