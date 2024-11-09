using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private IEnemyState currentState;
    public Transform player;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 5f;
    public float attackRange = 1f;
    public float sightRange = 10f;

    private void Start()
    {
        ChangeState(new PatrolState());  // Начнем с патрулирования
    }

    private void Update()
    {
        currentState?.Update(this);  // Обновляем текущее состояние
    }

    public void ChangeState(IEnemyState newState)
    {
        currentState?.Exit(this);  // Выход из старого состояния
        currentState = newState;   // Переход к новому состоянию
        currentState.Enter(this);  // Вход в новое состояние
    }

    // Методы для реализации логики врага
    public void Patrol()
    {
        // Патрулирование, например, перемещение по заранее заданному маршруту
        transform.Translate(Vector2.up * patrolSpeed * Time.deltaTime);
    }

    public void ChasePlayer()
    {
        // Преследование игрока
        Vector3 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * chaseSpeed * Time.deltaTime);
    }

    public void AttackPlayer()
    {
        // Логика атаки, например, нанесение урона игроку
        Debug.Log("Attacking player");
    }

    public bool CanSeePlayer()
    {
        // Проверка, видит ли враг игрока (например, через расстояние или проверку линии зрения)
        return Vector2.Distance(transform.position, player.position) <= sightRange;
    }

    public bool IsPlayerInAttackRange()
    {
        // Проверка, в пределах ли игрок для атаки
        return Vector2.Distance(transform.position, player.position) <= attackRange;
    }
}
