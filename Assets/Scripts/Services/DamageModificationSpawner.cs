using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static DamageModification;
using Random = UnityEngine.Random;

public class DamageModificationSpawner : MonoBehaviour, IService
{
    public bool spawnDinamically = false;
    public float Interval = 2f;
    public float StartDelay = 1f;
    public SpawnPoint[] dinamicSpawnPoints;        // Места, где могут появляться модификации
    public Transform DamageModificationPparent;
    public DamagePickup damagePickupPrefab;  // Префаб для спавна модификации
    public SpawnPoint[] hardSpawnPoints;
    public ChooseOnePickup ChooseOnePickup;
    public DMS[] damageModificationsLists;  // Доступные модификации
    [System.Serializable]
    public class DMS
    {
        public Rarity Rarity;
        public DamageModification[] availableModifiers;
    }
    void Start()
    {
        foreach(var  point in hardSpawnPoints)
        {
            SpawnModifier(point);
        }
        if(spawnDinamically)
            InvokeRepeating(nameof(SpawnModifierRandomPlace), StartDelay, Interval); // Спавним каждые 5 секунд
    }
    // Метод для спавна модификации
    public void SpawnModifierRandomPlace()
    {
        // Случайно выбираем точку спавна и модификацию
        SpawnPoint spawnPoint = dinamicSpawnPoints[Random.Range(0, dinamicSpawnPoints.Length)];
        SpawnModifier(spawnPoint);
    }
    public void SpawnModifier(SpawnPoint spawnPoint)
    {
        DamageModification damageModification = GetDamageModification(spawnPoint);
        if(damageModification==null)
        {
            Debug.Log("damageModification is null");
        }
        // Создаем объект модификации на сцене
        if (CanSpawn(spawnPoint))
        {
            GameObject modifierObject = Spawn(spawnPoint, out DamagePickup pickup);
            if (pickup != null)
            {
                pickup.SetDamageModification (damageModification);
            }
        }
    }
    private bool CanSpawn(SpawnPoint spawnPoint)
    {
        if (spawnPoint.IsOccupied())
        {
            return false;
        }
        else
            return true;
    }
    public GameObject Spawn(SpawnPoint spawnPoint, out DamagePickup damagePickup)
    {
        Spawn(spawnPoint.transform, out damagePickup);
        spawnPoint.Occupie(damagePickup);
        return damagePickup.gameObject;
    }
    public GameObject SpawnChooseOne(Transform where, SpawnParametrs spawnParametrs = null)
    {
        GameObject modifierObject = Instantiate(ChooseOnePickup.gameObject, where.position, Quaternion.identity, DamageModificationPparent);
        return modifierObject;
    }
        public GameObject Spawn(Transform where, out DamagePickup damagePickup, SpawnParametrs spawnParametrs = null)
    {
        if(spawnParametrs!=null)
        {
            if (spawnParametrs.chance < Random.Range(0, 100))
            {
                damagePickup = null;
                return null;
            }
        }
        GameObject modifierObject = Instantiate(damagePickupPrefab.gameObject, where.position, Quaternion.identity, DamageModificationPparent);
        damagePickup = modifierObject.GetComponent<DamagePickup>();
        damagePickup.SetDamageModification(GetDamageModification(Rarity.rare));
        return modifierObject;
    }
    private DamageModification GetDamageModification(SpawnPoint spawnPoint)
    {
       return GetDamageModification(spawnPoint.Rarity);
    }

    private DamageModification GetDamageModification(Rarity rarity)
    {
        foreach (DMS damageModificationsList in damageModificationsLists)
        {
            Debug.Log("rarity" + rarity.ToString() + " damageModificationsList.Rarity" + damageModificationsList.Rarity.ToString()  );

            if (damageModificationsList.Rarity.ToString() == rarity.ToString())
            {

                int num = Random.Range(0, damageModificationsList.availableModifiers.Length);
                DamageModification damageModification = damageModificationsList.availableModifiers[num];
                Debug.Log("num" + num + " damageModificationsList.availableModifiers.Length" + damageModificationsList.availableModifiers.Length + " damageModification" + damageModification.rarity.ToString());

                if (damageModification == null)
                    Debug.Log("damageModification is null");
                else
                {
                    Debug.Log("damageModification rariry" + damageModification.rarity.ToSafeString());
                }
                return damageModification;
            }
        }
        Debug.Assert(false,"DamageModification not found for rarity" + rarity);
        return null;
    }
}
