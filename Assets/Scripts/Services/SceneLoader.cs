using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public static class SceneLoader
{
    private class LoadingMonoBehaviour : MonoBehaviour { }
    private static AsyncOperation loadingAsyncOperation;
    public enum Scene
    {
        Level1,
        Level2,
        Level3,
        EndlessMode,
        Loading,
        MainMenu,
    }
    public static Scene CurrentScene;
    private static Action onLoaderCallback;
    public static void Load(Scene scene)
    {
        onLoaderCallback = () =>
        {
            GameObject loadingGO = new GameObject("loadingGO");
            loadingGO.AddComponent<LoadingMonoBehaviour>().StartCoroutine(LoadSceneAsync(scene));
            
        };
        if (scene == Scene.EndlessMode)
        {
            GameSettings.EndlessMode = true;
        }
        else
        {
            GameSettings.EndlessMode = false;
        }
        SceneManager.LoadScene(Scene.Loading.ToString());
    }
    private static IEnumerator LoadSceneAsync(Scene scene)
    {
        yield return null;
        loadingAsyncOperation = SceneManager.LoadSceneAsync(scene.ToString());
        CurrentScene = scene;
        while (!loadingAsyncOperation.isDone)
        {
            yield return null;
        }
    }
    public static float GetLoadingProgress()
    {
        if (loadingAsyncOperation == null) return 1f;
        return loadingAsyncOperation.progress;
    }
    public static void LoaderCallback()
    {
        if (onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }
}
