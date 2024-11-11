using UnityEngine;

[CreateAssetMenu(fileName = "ElectricDamageComponent", menuName = "DamageComponents/Electric")]
public class ElectricDamageComponent : DamageComponent
{
    public ElectricDamageComponent()
    {
        damageType = DamageType.electic;
    }
    public float stunDuration;
    public override void ApplyDamage(IDamageable damageable)
    {
        damageable.ApplyDamage(baseDamage);
        if (damageable is IStunable stunable)
        {
            stunable.Stun(stunDuration);
        }
    }
    public void ResetStats()
    {
        baseDamage = 2f;
        stunDuration = 0.5f;
    }
}
