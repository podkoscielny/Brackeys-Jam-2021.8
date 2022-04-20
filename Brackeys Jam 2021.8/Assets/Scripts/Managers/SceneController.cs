using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SceneController : MonoBehaviour
{
    public static event Action OnGameStart;
    public static event Action OnSceneChange;

    private Animator animator;

    void Start() => animator = GetComponent<Animator>();

    public void GoToMenu() => LoadScene("Menu");

    public void GoToGameplay() => LoadScene("Main");

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void MakeSceneReady() => OnGameStart?.Invoke(); //Invoke in animation trigger

    private void LoadScene(string sceneName)
    {
        OnSceneChange?.Invoke();
        HideSceneLoader();
        MasterVolume.Mute(() => SceneManager.LoadSceneAsync(sceneName));
    }

    private void HideSceneLoader() => animator.SetTrigger("Hide");
}
