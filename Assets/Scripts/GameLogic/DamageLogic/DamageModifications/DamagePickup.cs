using UnityEngine;
[RequireComponent (typeof(Collider2D))]

[CreateAssetMenu(fileName = "DamagePickup", menuName = "Damage/DamagePickup")]
public class DamagePickup : MonoBehaviour
{
    public DamageModification damageModification; // Модификация урона, которую игрок может подобрать

    // Метод для активации модификации при взаимодействии с игроком
    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player= other.GetComponent<Player>();
        if (player != null)
        {
            Weapon  playerWeapon = player.GetWeapon();
            damageModification.ApplyModification(playerWeapon);
            Destroy(gameObject);
        }

        // Удаляем объект модификации с карты (или деактивируем его)
    }
}
