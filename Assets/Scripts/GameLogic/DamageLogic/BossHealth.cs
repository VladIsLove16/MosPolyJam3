using System.Collections.Generic;
using UnityEngine;

public class BossHealth : HealthComponent
{
    [SerializeField]
    List<Collider2D> Shields;
    public override void ApplyDamage(DamageParameters damageParameters)
    {
        Debug.Log("damageParameters.enemyCollision" + damageParameters.enemyCollision + " other:" + damageParameters.enemyCollision.otherCollider);
        if (Shields.Contains(damageParameters.enemyCollision.otherCollider) || Shields.Contains(damageParameters.enemyCollision.collider))
        {
            SoundManager.PlaySound(SoundManager.Sound.BossGetDamaged);
            return;

        }
        TakeDamage(damageParameters, out bool Damaged);
    }
}