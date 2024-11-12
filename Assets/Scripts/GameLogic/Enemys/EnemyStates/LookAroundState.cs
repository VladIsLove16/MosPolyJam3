using UnityEngine;

public class LookAroundState : IEnemyState
{
    public LookAroundState(float time)
    {
        lookAroundTimer = time; 
    }
    public float lookAroundTimer;
    public void Enter(EnemyAI enemy)
    {
        enemy.GetEnemyMover().Stop();
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
