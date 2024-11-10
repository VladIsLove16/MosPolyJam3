using UnityEngine;
public class ChaseState : IEnemyState
{
    public void Enter(EnemyAI enemy)
    {
        Debug.Log("Entering Chase State");
    }

    public void Update(EnemyAI enemy)
    {
        if (enemy.IsPlayerInAttackRange())
        {
            enemy.ChangeState(new AttackState());
        }
        else if (!enemy.CanSeePlayer())
        {
            // Если игрок вышел из зоны видимости, враг идет к последней известной позиции
            enemy.ChangeState(new LookAroundState());
        }
        else
        {
            enemy.ChasePlayer();
        }
    }

    public void Exit(EnemyAI enemy)
    {
        Debug.Log("Exiting Chase State");
    }
}
