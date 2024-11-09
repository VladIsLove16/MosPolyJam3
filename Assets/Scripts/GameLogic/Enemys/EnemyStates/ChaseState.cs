using UnityEngine;
public class ChaseState : IEnemyState
{
    public void Enter(EnemyAI enemy)
    {
        Debug.Log("Entering Chase State");
    }

    public void Update(EnemyAI enemy)
    {
        // Логика преследования игрока
        enemy.ChasePlayer();

        // Если игрок слишком далеко, вернуться к патрулированию
        if (!enemy.CanSeePlayer())
        {
            enemy.ChangeState(new PatrolState());
        }

        // Если враг слишком близко, перейти к атаке
        if (enemy.IsPlayerInAttackRange())
        {
            enemy.ChangeState(new AttackState());
        }
    }

    public void Exit(EnemyAI enemy)
    {
        Debug.Log("Exiting Chase State");
    }
}
