using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class ScenesManager : MonoBehaviour
{
    [Header("INFORMATION")]
    [SerializeField] private string currentScene;
    [SerializeField] private string nextScene;

    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene().name;
        nextScene = currentScene;
    }

    public void SetNextScreen(string SceneName)
    {
        nextScene = SceneName;
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(nextScene);
    }

    public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(currentScene);
    }

    public void Exit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
