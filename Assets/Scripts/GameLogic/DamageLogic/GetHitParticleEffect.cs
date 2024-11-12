using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHitParticleEffect : MonoBehaviour
{
    [SerializeField] private ObjectPool steamPool; // Ссылка на пул объектов пара
    [SerializeField] private Vector3 offset = Vector3.zero; // Смещение для точки появления
    [SerializeField] private float effectDuration = 2f; // Длительность эффекта пара
    public void Emit(Collision2D collision)
    {
        // Получаем точку столкновения
        Vector3 contactPoint = GetContactPoint(collision);
        // Проверяем, есть ли у пули Rigidbody, чтобы получить направление её движения
        Rigidbody2D bulletRb = collision.rigidbody;
        if (bulletRb != null)
        {
            // Направление частиц будет противоположно направлению полёта пули
            Vector3 incomingDirection = bulletRb.velocity.normalized;

            // Получаем объект пара из пула
            GameObject steamEffect = steamPool.GetFromPool();
            if (steamEffect == null)
            {
                return;
            }
            steamEffect.transform.position = contactPoint + offset;

            // Поворачиваем частицы в направлении, противоположном incomingDirection
            steamEffect.transform.forward = -incomingDirection;

            // Запускаем корутину для возврата объекта в пул после завершения эффекта
            StartCoroutine(ReturnToPoolAfterDelay(steamEffect, effectDuration));
        }
    }

    private IEnumerator ReturnToPoolAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (steamPool != null)
            steamPool.ReturnToPool(obj);
        else
            Destroy(obj);
    }
    private Vector3 GetContactPoint(Collision2D collision)
    {
        int count = collision.contacts.Length;
        int number = count / 2;
        Vector3 contactPoint = collision.contacts[number].point;
        return contactPoint;
    }
}