﻿using UnityEngine;
[RequireComponent (typeof(Collider2D))]

public class DamagePickup : MonoBehaviour
{
    public DamageModification damageModification; // Модификация урона, которую игрок может подобрать

    // Метод для активации модификации при взаимодействии с игроком
    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player= other.GetComponent<Player>();
        if (player != null)
        {
            DamageApplier  playerWeapon = player.GetDamageApplier();
            damageModification.ApplyModification(playerWeapon);
            Destroy(gameObject);
        }

        // Удаляем объект модификации с карты (или деактивируем его)
    }
}
