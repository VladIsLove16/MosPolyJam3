using UnityEngine;

public class LookAroundState : IEnemyState
{
    private float lookAroundTimer;

    public void Enter(EnemyAI enemy)
    {
        lookAroundTimer = enemy.lookAroundTime;
        Debug.Log("Entering LookAround State");
    }

    public void Update(EnemyAI enemy)
    {
        if (lookAroundTimer > 0)
        {
            lookAroundTimer -= Time.deltaTime;
        }
        else
        {
            // После осматривания враг переходит в патрулирование
            enemy.ChangeState(new PatrolState());
        }
    }

    public void Exit(EnemyAI enemy)
    {
        Debug.Log("Exiting LookAround State");
    }
}
