using System;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private List<Vector2> patrolpoints = new();
    private bool randomPatrolPoints = false;
    public int randomPatrolPointschance = 10;
    private Vector2 lastSeenPlayerPosition;
    private EnemyMover enemyMover;
    private bool playerInSight;
    Weapon weapon;
    public float Radius = 5f;
    protected virtual void Start()
    {
        enemyMover = GetComponent<EnemyMover>();
        weapon = GetComponentInChildren<Weapon>();
        player = ServiceLocator.Current.Get<Player>();

        InitPatrolTargetSelection();
        SetNewPatrolTarget();
        ChangeState(new PatrolState());
    }

    private void InitPatrolTargetSelection()
    {
        if (randomPatrolPointschance > UnityEngine.Random.Range(0, 100))
        {
            randomPatrolPoints = true;
        }
        else
        {
            AddPotentialPoint((Vector2)transform.position + Vector2.up * Radius);
            AddPotentialPoint((Vector2)transform.position + Vector2.down * Radius);
            AddPotentialPoint((Vector2)transform.position + Vector2.right * Radius);
            AddPotentialPoint((Vector2)transform.position + Vector2.left * Radius);
            AddPotentialPoint((Vector2)transform.position);
        }
    }

    private void AddPotentialPoint(Vector2 potentialTarge)
    {
        NavMeshHit hit;
        Debug.Log("AddPotentialPoint "+ potentialTarge);
        if(NavMesh.SamplePosition(potentialTarge, out hit, 2f, NavMesh.AllAreas))
        {
            patrolpoints.Add(hit.position);
            Debug.Log("AddPotentialPoint TRUE");
        }
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
        if(!randomPatrolPoints)
        {
            SetTarget(patrolpoints);
            return;
        }
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
                    validTarget = true;
                    SetTarget(hit.position);
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

    private void SetTarget(Vector2 vector2)
    {
        patrolTarget = vector2;// Устанавливаем ближайшую доступную точку
        lastTimeSetNewPatrolTarget = DateTime.Now;
        return ;
    }

    private void SetTarget(List<Vector2> patrolpoints)
    {
        int count = patrolpoints.Count;
        Vector2 target = patrolpoints[UnityEngine.Random.Range(0, count)];
        SetTarget(target);
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
