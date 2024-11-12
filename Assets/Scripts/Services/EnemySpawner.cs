using UnityEngine;

public class EnemySpawner : MonoBehaviour, IService
{
    public bool spawnDinamically = false;
    public float Interval = 2f;
    public float StartDelay = 1f;
    public SpawnPoint[] dynamicspawnPoints; 
    public SpawnPoint[] hardspawnPoints;  // Точки спавна
    public float Radius;
    [SerializeField]
    public CharacterCreator CharacterCreator;
    [SerializeField]
    public GameObject EnemysParent;
    public float TotalHealth = 10f;

    // Метод, который будет вызываться для спавна каждого предмета
    public virtual void SpawnItem(SpawnPoint spawnPoint)
    {
        Spawn(spawnPoint, out SpawnObject itemObject);
    }
    public void StartSpawnDinamically()
    {
        InvokeRepeating(nameof(SpawnRandomItem), StartDelay, Interval);
    }
    protected virtual void Start()
    {
        // Спавним на всех точках сразу
        foreach (var point in hardspawnPoints)
        {
            if(point!=null)
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
    public virtual GameObject Spawn(SpawnPoint spawnPoint, out SpawnObject itemObject)
    {
        return Spawn(spawnPoint.transform, out itemObject);
    }
    public virtual GameObject Spawn(Transform spawnPoint, out SpawnObject itemObject, SpawnParametrs spawnParametrs = null)
    {
        if (spawnParametrs != null)
        {
            if (spawnParametrs.chance < Random.Range(0, 100))
            {
                itemObject = null;
                return null;
            }
        }
        GameObject newCharacter = CharacterCreator.GetNewCharacter();
        HealthComponent healthComponent = newCharacter.GetComponent<HealthComponent>();
        if (spawnDinamically)
        {
            Debug.Log("DinamicSpawn");
            healthComponent.CurrentHealth = TotalHealth;
            healthComponent.SetMaxHealth(TotalHealth);
        }
        GameObject spawnedItem = Instantiate(newCharacter,(Vector2) spawnPoint.transform.position, Quaternion.identity, EnemysParent.transform);
        spawnedItem.gameObject.SetActive(true);
        itemObject = spawnedItem.GetComponent<SpawnObject>();
        return spawnedItem;
    }

    internal float GetHealth()
    {
        return TotalHealth;
    }

    internal void IncreaseHealth(float totalHealth)
    {
        TotalHealth = totalHealth;
    }
}