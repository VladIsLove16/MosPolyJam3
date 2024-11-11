using UnityEngine;
using System.Collections.Generic;
using System;
public class DamageComponent : ScriptableObject
{
    public Sprite Sprite;
    public float baseDamage;
    public DamageType damageType;
    public float CalculatedDamage;
    protected List<float> additiveModifiers = new List<float>();
    protected List<float> multiplicativeModifiers = new List<float>();
    public Action<DamageComponent> damageChanged;
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

    public void AddAdditiveModifier(float value)
    {
       additiveModifiers.Add(value);
       float dmg = CalculateDamage();
        damageChanged?.Invoke(this);
        Debug.Log("AddAdditiveModifier To " + damageType  + "add" + additiveModifiers.Count);
        Debug.Log("dmg" + dmg);
    }
    public void AddMultiplicativeModifier(float value)
    {
        multiplicativeModifiers.Add(value);
        CalculateDamage();

        damageChanged?.Invoke(this);
    }

    public virtual void ApplyDamage(IDamageable damageable)
    {
        damageable.ApplyDamage(CalculateDamage());
    }

    public string GetDescription()
    {
        float additiveDamage = 0;
        float multiplierDamage = 1;
        foreach (var additive in additiveModifiers)
            additiveDamage += additive;
        foreach (var multiplier in multiplicativeModifiers)
            multiplierDamage += multiplier;
        float CalculatedDamage =  CalculateDamage();
        return $"({baseDamage}+{additiveDamage})*{multiplierDamage} = {CalculatedDamage} ";
    }
}
