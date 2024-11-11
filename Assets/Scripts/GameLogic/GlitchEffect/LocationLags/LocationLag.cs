using UnityEngine;

public class LocationLag : MonoBehaviour
    {
        Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void StartLags()
    {
        animator.SetTrigger("Lags");
    }
}
