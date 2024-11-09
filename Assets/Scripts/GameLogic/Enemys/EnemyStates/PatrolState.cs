using UnityEngine;
public class PatrolState : IEnemyState
{
    public void Enter(EnemyAI enemy)
    {
        Debug.Log("Entering Patrol State");
        // Начало патрулирования (например, назначить цели)
    }

    public void Update(EnemyAI enemy)
    {
        // Логика патрулирования (например, перемещение вдоль пути)
        enemy.Patrol();

        // Если игрок в пределах видимости, переход к преследованию
        if (enemy.CanSeePlayer())
        {
            enemy.ChangeState(new ChaseState());
        }
    }

    public void Exit(EnemyAI enemy)
    {
        Debug.Log("Exiting Patrol State");
    }
}
