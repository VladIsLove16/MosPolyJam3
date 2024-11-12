using UnityEngine;

public class AttackState : IEnemyState
{
    public void Enter(EnemyAI enemy)
    {
        Debug.Log("Entering Attack State");
        enemy.GetEnemyMover().Stop();
        // Можно добавить анимацию атаки
    }

    public void Update(EnemyAI enemy)
    {
        // Логика атаки
        enemy.AttackPlayer();

        // Если игрок выходит из зоны атаки, вернуться к преследованию
        if (!enemy.IsPlayerInAttackRange())
        {
            enemy.ChangeState(new ChaseState());
        }
    }

    public void Exit(EnemyAI enemy)
    {
        Debug.Log("Exiting Attack State");
    }
}
