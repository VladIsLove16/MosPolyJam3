using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "PoisonDamageComponent", menuName = "DamageComponents/Poison")]
public class PoisonDamageComponent : DamageComponent
{
    public float duration;    // Длительность эффекта
    public float tickInterval; // Интервал между тиками
    private float damage;
    public PoisonDamageComponent ()
    {
        damageType = DamageType.poison;
    }
    // Начать эффект яда
    private IEnumerator ApplyPoisonEffect(IDamageable target)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            // Здесь вызываем функцию для нанесения урона по цели
            target.ApplyDamage(damage);
            yield return new WaitForSeconds(tickInterval);
            elapsedTime += tickInterval;
        }
    }

    public override void ApplyDamage(IDamageable damageable)
    {
        damage = CalculateDamage();
        ApplyPoisonEffect(damageable);
    }
}
