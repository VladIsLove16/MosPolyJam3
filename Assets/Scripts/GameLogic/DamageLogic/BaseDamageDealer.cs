using UnityEngine;

public class BaseDamageDealer : MonoBehaviour
{
    private DamageApplier damageApplier;
    public float DefaultDamage=5f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        HealthComponent healthComponent = collision.gameObject.GetComponent<HealthComponent>();
        Debug.Log("BaseDamageDealer OnCollisionEnter2D");
        if (healthComponent !=null)
        {

            Debug.Log("BaseDamageDealer OnTriggerEnter2D" + damageApplier.CalculateDamage());
            if (damageApplier == null)
            {
                healthComponent.TakeDamage(new DamageParameters() { enemyCollision = collision, damage = DefaultDamage },out bool damaged );
            }
            else
            {
                damageApplier.ApplyDamage(new DamageParameters() { enemyCollision = collision },healthComponent);
            }
        }
        Destroy(gameObject);
    }
    public void SetDamageApplier(DamageApplier damageApplier)
    {
        this.damageApplier = damageApplier;
    }
}
