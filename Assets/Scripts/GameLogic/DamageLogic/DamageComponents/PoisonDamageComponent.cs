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
    public void ResetStats()
    {
        duration = 2f;
        tickInterval = 0.5f;
        damage = 0.5f;
        baseDamage = 0.5f;
    }
}
