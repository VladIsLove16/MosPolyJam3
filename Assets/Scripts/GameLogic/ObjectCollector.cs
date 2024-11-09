using System.Collections.Generic;
using UnityEngine;

public class ObjectCollector : MonoBehaviour
{
    [Header("World Objects")]
    private List<GameObject> worldObjectsOnScreen=new List<GameObject>();  // Список для хранения объектов мира с коллайдерами

    void Start()
    {
        worldObjectsOnScreen = new List<GameObject>();
        CollectObjectsOnScreen();
    }

    public List<GameObject> CollectObjectsOnScreen()
    {
        worldObjectsOnScreen.Clear();
        // Получаем все объекты в сцене с коллайдерами
        Collider[] colliders = FindObjectsOfType<Collider>();  // Получаем все объекты с коллайдерами на сцене
        foreach (Collider collider in colliders)
        {
            // Проверяем, что объект находится в пределах экрана
            if (IsObjectVisible(collider.gameObject))
            {
                // Если объект видим и имеет коллайдер, добавляем его в список
                worldObjectsOnScreen.Add(collider.gameObject);
            }
        }
        return worldObjectsOnScreen;
    }

    bool IsObjectVisible(GameObject obj)
    {
        // Проверяем, виден ли объект с камеры
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);  // Получаем плоскости фрустрама камеры
            return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);  // Проверяем, пересекается ли AABB объекта с плоскостями камеры
        }
        return false;
    }
}
