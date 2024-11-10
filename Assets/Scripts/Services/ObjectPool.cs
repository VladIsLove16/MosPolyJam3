using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab; // Префаб для пуллинга
    [SerializeField] private int poolSize = 10; // Начальный размер пула

    private Queue<GameObject> pool = new Queue<GameObject>();

    private void Start()
    {
        // Создаём начальный пул объектов
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false); // Делаем объект неактивным, пока он не нужен
            pool.Enqueue(obj);
        }
    }

    public GameObject GetFromPool()
    {
        // Проверяем, есть ли доступный объект в пуле
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            // Если пул пуст, создаем новый объект
            GameObject obj = Instantiate(prefab);
            obj.SetActive(true);
            return obj;
        }
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
