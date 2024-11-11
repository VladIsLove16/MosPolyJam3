using UnityEngine;
[System.Serializable]
public class SpawnPoint : MonoBehaviour
{
    public Rarity Rarity;
    ISpawnObject spawnObject;
    public int randomRarityChance = 30;
    public bool randomRarity;
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
    public void Occupie(ISpawnObject spawnObject)
    {
        this.spawnObject = spawnObject;
    }
    internal bool IsOccupied()
    {
        bool occupied = spawnObject != null;
        return occupied;
    }
    
}
