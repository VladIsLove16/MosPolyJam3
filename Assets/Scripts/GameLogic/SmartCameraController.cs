using UnityEngine;

public class SmartCameraController : MonoBehaviour
{
    [SerializeField] private Transform player; // Ссылка на объект игрока
    [SerializeField] private float thresholdX = 2f; // Ширина центрального квадрата
    [SerializeField] private float thresholdY = 2f; // Высота центрального квадрата
    [SerializeField] private float cameraSpeed = 2f; // Скорость перемещения камеры
    [SerializeField] private float bufferDistance = 0.5f; // Дополнительное расстояние за границами квадрата

    private Vector3 cameraOffset; // Смещение камеры относительно игрока

    private void Start()
    {
        // Инициализируем смещение камеры на старте
        cameraOffset = transform.position - player.position;
    }

    private void LateUpdate()
    {
        // Рассчитываем целевую позицию камеры на основе позиции игрока и смещения
        Vector3 targetPosition = player.position + cameraOffset;

        // Определяем расстояние между текущей позицией камеры и целевой позицией
        Vector3 delta = targetPosition - transform.position;

        // Проверяем, выходит ли игрок за границы квадрата с учетом буфера
        if (Mathf.Abs(delta.x) > thresholdX + bufferDistance || Mathf.Abs(delta.y) > thresholdY + bufferDistance)
        {
            // Смещаем камеру в направлении игрока с плавной интерполяцией
            transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSpeed * Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        if (player == null) return;

        // Рассчитываем центральную точку для рисования квадрата
        Vector3 center = transform.position - cameraOffset;

        // Устанавливаем цвет для Gizmos
        Gizmos.color = Color.green;

        // Рисуем границы центрального квадрата
        Gizmos.DrawLine(new Vector3(center.x - thresholdX, center.y - thresholdY, center.z),
                        new Vector3(center.x + thresholdX, center.y - thresholdY, center.z));
        Gizmos.DrawLine(new Vector3(center.x + thresholdX, center.y - thresholdY, center.z),
                        new Vector3(center.x + thresholdX, center.y + thresholdY, center.z));
        Gizmos.DrawLine(new Vector3(center.x + thresholdX, center.y + thresholdY, center.z),
                        new Vector3(center.x - thresholdX, center.y + thresholdY, center.z));
        Gizmos.DrawLine(new Vector3(center.x - thresholdX, center.y + thresholdY, center.z),
                        new Vector3(center.x - thresholdX, center.y - thresholdY, center.z));
    }
}
