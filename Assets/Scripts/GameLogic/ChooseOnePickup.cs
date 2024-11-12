using System.Collections.Generic;
using UnityEngine;

public class ChooseOnePickup : MonoBehaviour
{
    public List<GameObject> OthergameObjects;
    public List<SpawnPoint> OthergameObjectPoints;
    private void Awake()
    {
        foreach (var obj in OthergameObjectPoints)
        {
            obj.Occupied +=() => OnOccupi(obj);
        }
    }
    private void OnOccupi(SpawnPoint spawnPoint)
    {
        SpawnObject gameObject = spawnPoint.spawnObject;
        gameObject.Taken += OnTaken;
    }
    public void OnTaken()
    {
        foreach (var a in OthergameObjectPoints)
        {
            if(a!=null)
                if(a.spawnObject!=null)
                    Destroy(a.spawnObject.gameObject);
        }
    }
}