using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "PoisonDamageComponent", menuName = "DamageComponents/Poison")]
public class PoisonDamageComponent : DamageComponent
{
    public float duration = 2f;    // Длительность эффекта
    public float tickInterval = 0.5f; // Интервал между тиками
    private float damage = 0.5f;
    public PoisonDamageComponent ()
    {
        damageType = DamageType.poison;
    }
    // Начать эффект яда
    private IEnumerator ApplyPoisonEffect(IDamageable target, DamageParameters damageParameters)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            target.ApplyDamage(damageParameters);
            yield return new WaitForSeconds(tickInterval);
            elapsedTime += tickInterval;
        }
    }

    public override void ApplyDamage(IDamageable damageable, DamageParameters damageParameters)
    {
        base.ApplyDamage(damageable, damageParameters);
        ApplyPoisonEffect(damageable, damageParameters);
    }
    public override void ResetStats()
    {
        base.ResetStats();
    }
}
