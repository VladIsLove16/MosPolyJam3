using System;
using UnityEngine;
[System.Serializable]
public class SpawnPoint : MonoBehaviour
{
    public Rarity Rarity;
    public SpawnObject spawnObject;
    public int randomRarityChance = 30;
    public bool randomRarity;
    public Action Occupied;
    private void Awake()
    {
        if (randomRarityChance > UnityEngine.Random.Range(0, 100)) ;
        {
            randomRarity = true;
        }
        if(randomRarity)
        {
            Rarity = RarityGetter.GetRandomRarity();
        }
    }
    public void Occupie(SpawnObject spawnObject)
    {
        this.spawnObject = spawnObject;
        Occupied?.Invoke();
    }
    internal bool IsOccupied()
    {
        bool occupied = spawnObject != null;
        return occupied;
    }
    
}
