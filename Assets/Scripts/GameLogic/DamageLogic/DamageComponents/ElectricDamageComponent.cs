using UnityEngine;

[CreateAssetMenu(fileName = "ElectricDamageComponent", menuName = "DamageComponents/Electric")]
public class ElectricDamageComponent : DamageComponent
{
    public ElectricDamageComponent()
    {
        damageType = DamageType.electic;
    }
    public float stunDuration;
    public override void ApplyDamage(IDamageable damageable, DamageParameters damageParameters)
    {
        base.ApplyDamage(damageable, damageParameters);
        if (damageable is IStunable stunable)
        {
            stunable.Stun(stunDuration);
            SoundManager.PlaySound(SoundManager.Sound.ElecticDamage);
        }
    }
    public override void ResetStats()
    {
        base.ResetStats();
    }
}
