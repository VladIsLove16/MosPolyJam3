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
            GameObject obj = Instantiate(prefab,transform,false);
            obj.SetActive(false); // Делаем объект неактивным, пока он не нужен
            pool.Enqueue(obj);
        }
    }
    private void OnDestroy()
    {
         foreach(GameObject obj in pool)
        {
            Destroy(obj,0.5f);
        }
    }
    private void DestroyD(GameObject game)
    {

    }
    public GameObject GetFromPool()
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
            return null;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
