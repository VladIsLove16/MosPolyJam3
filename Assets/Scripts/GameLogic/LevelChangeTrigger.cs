using System.Collections;
using UnityEngine;

public class LevelChangeTrigger : MonoBehaviour
{
    public SceneLoader.Scene scene;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            ServiceLocator.Current.Get<EventBus>().Invoke<SceneChanged>(new SceneChanged(scene));
            SceneLoader.Load(scene);
        }
    }
}
