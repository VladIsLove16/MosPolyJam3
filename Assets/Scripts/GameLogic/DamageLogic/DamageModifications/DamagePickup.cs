using UnityEngine;
[RequireComponent (typeof(Collider2D))]

public class DamagePickup : MonoBehaviour, ISpawnObject
{

    private DamageModification damageModification; // Модификация урона, которую игрок может подобрать

    // Метод для активации модификации при взаимодействии с игроком
    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player= other.GetComponent<Player>();
        if (player != null)
        {
            DamageApplier damageApplier = player.GetDamageApplier();
            damageModification.ApplyModification(damageApplier);
            Destroy(gameObject);
        }

        // Удаляем объект модификации с карты (или деактивируем его)
    }
    public void SetDamageModification(DamageModification damageModification)
    {
        this.damageModification = damageModification;
        Debug.Log("Setted SetDamageModification" + damageModification.ToString());
    }
}
