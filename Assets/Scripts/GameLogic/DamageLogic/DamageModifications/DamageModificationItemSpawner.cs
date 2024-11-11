using UnityEngine;
using Random = UnityEngine.Random;

public class DamageModificationItemSpawner : ItemSpawner
{
    public DamagePickup damagePickupPrefab;  // Префаб для спавна модификации
    public DMS[] availableModifiers;  // Доступные модификации

    [System.Serializable]
    public class DMS
    {
        public Rarity Rarity;
        public DamageModification[] availableModifiers;
    }

    // Реализация спавна для модификаций
    public override void SpawnItem(SpawnPoint spawnPoint)
    {
        DamageModification damageModification = GetDamageModification(spawnPoint);
        if(damageModification==null)
        {
            Debug.LogError("DamageModification not found for spawnPoint: " + spawnPoint);
        }
        // Создаем объект модификации на сцене
        if (CanSpawn(spawnPoint))
        {
            GameObject modifierObject = Spawn(spawnPoint, out ISpawnObject pickup);
            if (pickup != null)
            {
                DamagePickup damagePickup = pickup as DamagePickup;
                damagePickup.SetDamageModification(damageModification);
            }
            else
                Debug.Assert(true,"pickup is null");
        }
    }

    protected bool CanSpawn(SpawnPoint spawnPoint)
    {
        return !spawnPoint.IsOccupied();
    }

    // Метод для получения модификации на основе редкости точки спавна
    protected DamageModification GetDamageModification(SpawnPoint spawnPoint)
    {
        return GetDamageModification(spawnPoint.Rarity);
    }

    // Метод для получения модификации на основе редкости
    protected DamageModification GetDamageModification(Rarity rarity)
    {
        foreach (var modifier in availableModifiers)
        {
            if (modifier.Rarity == rarity)
            {
                int num = Random.Range(0, modifier.availableModifiers.Length);
                return modifier.availableModifiers[num];
            }
        }
        Debug.LogError("DamageModification not found for rarity: " + rarity);
        return null;
    }
}