using UnityEngine;

public class BaseDamageDealer : MonoBehaviour
{
    private DamageApplier damageApplier;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (damageApplier == null)
            return;

        HealthComponent healthComponent = collision.gameObject.GetComponent<HealthComponent>();
        if (healthComponent !=null)
        {
            damageApplier.ApplyDamage(healthComponent);
        }
        Destroy(gameObject);
    }
    public void SetDamageApplier(DamageApplier damageApplier)
    {
        this.damageApplier = damageApplier;
    }
}
