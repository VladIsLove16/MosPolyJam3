/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System;
using UnityEditor.SearchService;
using UnityEngine;

public class ScoreManager : MonoBehaviour, IService
{
    public int Score;

    public Action<int> OnScoreChanged;


    private void Awake()
    {
        ServiceLocator.Current.Get<EventBus>().Subscribe<SceneChanged>(OnSceneChange);
        switch (SceneLoader.CurrentScene)
        {
            case SceneLoader.Scene.Level4:
                SetScore(0);
                break;
            case SceneLoader.Scene.Level2:
                SetScore(PlayerPrefs.GetInt("Level1Score"));
                break;
            case SceneLoader.Scene.Level3:
                SetScore(PlayerPrefs.GetInt("Level2Score"));
                break;
        }
    }

    private void OnSceneChange(SceneChanged changed)
    {
        switch (changed.scene)
        {
            case SceneLoader.Scene.Level4:
                PlayerPrefs.SetInt("Level1Score", Score);
                break;
            case SceneLoader.Scene.Level2:
                PlayerPrefs.SetInt("Level2Score", Score);
                break;
            case SceneLoader.Scene.Level3:
                PlayerPrefs.SetInt("Level3Score", Score);
                break;
        }
    }

    public void AddScore(int score)
    {
        Score += score;
        OnScoreChanged?.Invoke(Score);
    }
    public void SetScore(int score)
    {
        Score = score;
        OnScoreChanged?.Invoke(Score);
    }

}