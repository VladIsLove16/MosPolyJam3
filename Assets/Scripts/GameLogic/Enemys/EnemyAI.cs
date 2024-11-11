using System;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent (typeof(EnemyMover))]
public class EnemyAI : MonoBehaviour
{
    public IEnemyState currentState;
    private Player player;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 5f;
    public float attackRange = 1f;
    public float sightRange = 10f;
    public float patrolRadius = 3f;
    public float lookAroundTime = 3f;
    public float setNewPatrolTargetDelay = 2f;
    public DateTime lastTimeSetNewPatrolTarget;

    private Vector2 patrolTarget;
    private Vector2 lastSeenPlayerPosition;
    private EnemyMover enemyMover;
    private bool playerInSight;
    Weapon weapon;

    protected virtual void Start()
    {
        enemyMover = GetComponent<EnemyMover>();
        SetNewPatrolTarget();
        ChangeState(new PatrolState());
        weapon  = GetComponentInChildren<Weapon>();
        player = ServiceLocator.Current.Get<Player>();
    }

    protected virtual void Update()
    {
        currentState?.Update(this);
    }

    public virtual void ChangeState(IEnemyState newState)
    {
        currentState?.Exit(this);
        currentState = newState;
        currentState.Enter(this);
    }
    public EnemyMover GetEnemyMover()
    {
        return enemyMover;
    }
    public void Patrol()
    {
        if (PatrolTargetChangedAlongTimeAgo())
            SetNewPatrolTarget();
        if (PatrolTargerReached())
        {
             ChangeState(new LookAroundState());
        }
        else
        {
            enemyMover.Move(patrolTarget, patrolSpeed); // Используем метод Move для патрулирования
        }
    }

    private bool PatrolTargetChangedAlongTimeAgo()
    {
        return DateTime.Now > lastTimeSetNewPatrolTarget.AddSeconds(setNewPatrolTargetDelay);
    }

    private bool PatrolTargerReached()
    {
        return Vector2.Distance(transform.position, patrolTarget) < enemyMover.StoppingDistance + 0.1f;
    }

    private void SetNewPatrolTarget()
    {
        Vector2 randomDirection;
        NavMeshHit hit;
        bool validTarget = false;
        int tryies = 0;
        int radiusTryies = 0;
        float patrolCurrentRad = patrolRadius;
        // Попытка найти доступную точку для патрулирования
        while (radiusTryies < 5)
        {   
            while (!validTarget && tryies < 100 )
            {
                randomDirection = UnityEngine.Random.insideUnitCircle * patrolCurrentRad;
                Vector2 potentialTarget = (Vector2)transform.position + randomDirection;
                // Проверяем, находится ли точка на NavMesh
                if (NavMesh.SamplePosition(potentialTarget, out hit, 2f, NavMesh.AllAreas))
                {
                    patrolTarget = hit.position; // Устанавливаем ближайшую доступную точку
                    lastTimeSetNewPatrolTarget = DateTime.Now;
                    validTarget = true;
                    return;
                }
                else
                tryies++;
            }
            radiusTryies++;
            tryies = 0;
            patrolCurrentRad = 0.01f;
        }
    }


    public void ChasePlayer()
    {
        lastSeenPlayerPosition = player.transform. position;
        enemyMover.Move(player .transform.position, chaseSpeed);
    }

    public void MoveToLastSeenPosition()
    {
        enemyMover.Move(lastSeenPlayerPosition, patrolSpeed); // Движение к последней позиции
    }
    public void AttackPlayer()
    {
        weapon.Shoot();
    }
    public bool CanSeePlayer()
    {
        return Vector2.Distance(transform.position, player. transform.position) <= sightRange;
    }

    public bool IsPlayerInAttackRange()
    {
        return Vector2.Distance(transform.position, player.transform.position) <= attackRange;
    }
}
