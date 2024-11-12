using UnityEngine;

public class BossFightTrigger : MonoBehaviour
{
    [SerializeField]
    BossAI bossAI;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("BossFightTrigger OnTriggerEnter2D" + collision.name);
        if (collision.GetComponent<Player>() != null)
        {
            bossAI.StartFight();
        }
    }
}