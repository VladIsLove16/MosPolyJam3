using UnityEngine;

public class BaseDamageDealer : MonoBehaviour
{
    private DamageApplier damageApplier;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (damageApplier == null)
            return;
        if (collision is IDamageable damageable)
        {
            damageApplier.ApplyDamage(damageable);
        }
    }
    public void SetDamageApplier(DamageApplier damageApplier)
    {
        this.damageApplier = damageApplier;
    }
}
