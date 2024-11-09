﻿using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageModification", menuName = "Damage/DamageModification")]
public class DamageModification : ScriptableObject
{
    public List<Modifier> modifiers = new List<Modifier>();
    [System.Serializable]
    public class Modifier
    {
        public ModifierType modifierType;
        public float modifierAmount;
        public DamageType damageType;
    }
    public Rarity rarity;
    public float spawnChance; // Вероятность появления модификации
    public void ApplyModification(Weapon weapon)
    {
        foreach (Modifier modifier in modifiers)
        {
            if(modifier.modifierType == ModifierType.Additive)
                weapon.GetDamageComponent(modifier.damageType).AddAdditiveModifier(modifier.modifierAmount);
            if(modifier.modifierType == ModifierType.Multiplicative)
                weapon.GetDamageComponent(modifier.damageType).AddMultiplicativeModifier(modifier.modifierAmount);
        }
    }
    //public void ApplyModification(DamageComponent targetDamageComponent)
    //{
    //    foreach (Modifier modifier in modifiers)
    //    {
    //        modifier.AddModifier(targetDamageComponent);
    //    }
    //}
}
