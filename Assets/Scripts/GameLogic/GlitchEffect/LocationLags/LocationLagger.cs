using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LocationLagger : MonoBehaviour
{
    List<LaggingObject> laggingObjects;
    private void Start()
    {
        laggingObjects = FindObjectsByType<LaggingObject>(FindObjectsSortMode.None).ToList();
    }
    public void StartLags()
    {
        foreach (var item in laggingObjects)
        {
            item.StartLags();
        }
    }
    public void StartLags(float delay)
    {
        Invoke(nameof(StartLags), delay);
    }
}
