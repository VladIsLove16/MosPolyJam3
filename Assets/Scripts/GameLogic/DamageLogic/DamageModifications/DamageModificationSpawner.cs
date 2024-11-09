using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class DamageModificationSpawner : MonoBehaviour
{
    public DamagePickup damagePickupPrefab;  // Префаб для спавна модификации
    public DamagePickupSpawnPoint[] spawnPoints;        // Места, где могут появляться модификации
    public DamageModification[] availableModifiers;  // Доступные модификации
    public float Interval = 2f;
    public float StartDelay = 1f;
    // Метод для спавна модификации
    public void SpawnModifier()
    {
        // Случайно выбираем точку спавна и модификацию
        DamagePickupSpawnPoint spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        DamageModification modifier = availableModifiers[Random.Range(0, availableModifiers.Length)];

        // Создаем объект модификации на сцене
        if (CanSpawn(spawnPoint))
        {
            GameObject modifierObject = Spawn(spawnPoint, out DamagePickup pickup);
            if (pickup != null)
            {
                pickup.damageModification = modifier;
            }
        }
    }
    private void Start()
    {
        InvokeRepeating(nameof(SpawnModifier), StartDelay, Interval); // Спавним каждые 5 секунд
    }
    private bool CanSpawn(DamagePickupSpawnPoint spawnPoint)
    {
        if(spawnPoint.IsOccupied())
        {
            return false;
        }
        else 
            return true;
    }
    private GameObject Spawn(DamagePickupSpawnPoint spawnPoint, out DamagePickup damagePickup )
    {
        GameObject modifierObject = Instantiate(damagePickupPrefab.gameObject, spawnPoint.transform.position, spawnPoint.transform.rotation);
        damagePickup = modifierObject.GetComponent<DamagePickup>();
        spawnPoint.Occupie(damagePickup);
        return modifierObject;
    }
}
