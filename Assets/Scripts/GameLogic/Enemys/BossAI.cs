using System;

public class BossAI : EnemyAI
{
    EnemySpawner spawner;
    public Action FightStarted;
    protected override void Awake()
    {
        base.Awake();
        spawner = ServiceLocator.Current.Get<EnemySpawner>();
    }
    protected override void Start()
    {
        base.Start();
        enemyMover.Stop();
        ChangeState(new LookAroundState(60f));
    }
    public void StartFight()
    {
        ChangeState(new ChaseState());
        StartSpawningEnemies();
        FightStarted?.Invoke();
    }

    protected override void Update()
    {
        base.Update();
    }
    public void StartSpawningEnemies()
    {
        spawner.StartSpawnDinamically();
    }
}
