﻿using UnityEngine;

public class EnemySpawner : MonoBehaviour, IService
{
    public bool spawnDinamically = false;
    public float Interval = 2f;
    public float StartDelay = 1f;
    public SpawnPoint[] dynamicspawnPoints; 
    public SpawnPoint[] hardspawnPoints;  // Точки спавна
    public GameObject[] itemPrefabs;  // Префаб для спавна предмета
    public float Radius;

    // Метод, который будет вызываться для спавна каждого предмета
    public virtual void SpawnItem(SpawnPoint spawnPoint)
    {
        Spawn(spawnPoint, out ISpawnObject itemObject);
    }

    protected virtual void Start()
    {
        // Спавним на всех точках сразу
        foreach (var point in hardspawnPoints)
        {
            SpawnItem(point);
        }

        if (spawnDinamically)
            InvokeRepeating(nameof(SpawnRandomItem), StartDelay, Interval);
    }

    // Метод для динамического спавна
    public void SpawnRandomItem()
    {
        // Случайно выбираем точку спавна
        SpawnPoint spawnPoint = dynamicspawnPoints[Random.Range(0, dynamicspawnPoints.Length)];
        SpawnItem(spawnPoint);
    }

    // Метод для спавна объекта на точке
    public virtual GameObject Spawn(SpawnPoint spawnPoint, out ISpawnObject itemObject)
    {
        return Spawn(spawnPoint.transform, out itemObject);
    }
    public virtual GameObject Spawn(Transform spawnPoint, out ISpawnObject itemObject, SpawnParametrs spawnParametrs = null)
    {
        if (spawnParametrs != null)
        {
            if (spawnParametrs.chance < Random.Range(0, 100))
            {
                itemObject = null;
                return null;
            }
        }
        GameObject spawnedItem = Instantiate(itemPrefabs[Random.Range(0, itemPrefabs.Length)], spawnPoint.transform.position, Quaternion.identity);
        itemObject = spawnedItem.GetComponent<ISpawnObject>();
        return spawnedItem;
    }
}