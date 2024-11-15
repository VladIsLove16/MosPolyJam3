﻿using UnityEngine;

[CreateAssetMenu(fileName = "PhysicalDamageComponent", menuName = "DamageComponents/PhysicalDamageComponent")]
public class PhysicalDamageComponent : DamageComponent
{
    public PhysicalDamageComponent()
    {
        damageType = DamageType.physical;
    }
    public override void ResetStats()
    {
        base.ResetStats();
    }
}
