using UnityEngine;

public class BossAI : EnemyAI
{
    private bool isSpawningEnemies = false;

    // Параметры для полета и спавна врагов
    [SerializeField] private float spawnRadius = 10f;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval = 5f;
    private float lastSpawnTime;

    protected override void Start()
    {
        base.Start();
        lastSpawnTime = Time.time;
    }

    protected override void Update()
    {
        base.Update();
        // Если время для спавна врагов
        if (Time.time > lastSpawnTime + spawnInterval && !isSpawningEnemies)
        {
            StartSpawningEnemies();
        }
    }
    public void StartSpawningEnemies()
    {
        isSpawningEnemies = true;
        lastSpawnTime = Time.time;
        // Спавним врагов вокруг босса
        Vector3 spawnPosition = transform.position + (Vector3)Random.insideUnitCircle * spawnRadius;
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        isSpawningEnemies = false;
    }
}
