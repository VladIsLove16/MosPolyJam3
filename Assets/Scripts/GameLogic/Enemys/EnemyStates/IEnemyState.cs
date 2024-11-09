using UnityEngine;
public interface IEnemyState
{
    void Enter(EnemyAI enemy);    // Вход в состояние
    void Update(EnemyAI enemy);   // Логика обновления состояния
    void Exit(EnemyAI enemy);     // Выход из состояния
}
