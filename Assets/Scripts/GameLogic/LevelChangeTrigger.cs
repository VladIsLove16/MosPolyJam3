using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChangeTrigger : MonoBehaviour
{
    public SceneLoader.Scene scene;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() !=null)
            SceneLoader.Load(scene);
    }
}
public class ChooseOnePuckup : MonoBehaviour
{
    public List<GameObject> OthergameObjects;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Player>() != null)
        {
            foreach(var a in OthergameObjects)
            {
                Destroy(a.gameObject);
            }
        }
    }
}