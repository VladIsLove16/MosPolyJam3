using UnityEngine;
using System.Collections.Generic;
using System;

public class DamageComponent : ScriptableObject
{
    public float baseDamage;
    public DamageType damageType;
    public float CalculatedDamage;
    protected List<float> additiveModifiers = new List<float>();
    protected List<float> multiplicativeModifiers = new List<float>();
    public DamageComponent()
    {
        CalculateDamage();
    }
    public DamageComponent(DamageType damageType) { this.damageType = damageType; CalculateDamage(); }

    protected float CalculateDamage()
    {
        float modifiedDamage = baseDamage;
        foreach (var additive in additiveModifiers)
            modifiedDamage += additive;
        foreach (var multiplier in multiplicativeModifiers)
            modifiedDamage *= multiplier;
        CalculatedDamage = modifiedDamage;
        return modifiedDamage;
    }

    public void AddAdditiveModifier(int value)
    {
       additiveModifiers.Add(value);
       float dmg = CalculateDamage();
        Debug.Log("AddAdditiveModifier To " + damageType  + "add" + additiveModifiers.Count);
        Debug.Log("dmg" + dmg);
    }
    public void AddMultiplicativeModifier(int value)
    {
        multiplicativeModifiers.Add(value);
        CalculateDamage();
    }

    public virtual void ApplyDamage(IDamageable damageable)
    {
        damageable.ApplyDamage(CalculateDamage());
    }
}
