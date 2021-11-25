using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static event Action OnGameStart;

    private Animator animator;

    private delegate void SceneSetterDelegate();
    private SceneSetterDelegate _sceneSetter;

    void Start() => animator = GetComponent<Animator>();

    public void GoToMenu()
    {
        _sceneSetter = LoadMenuScene;
        HideSceneLoader();
    }

    public void GoToGameplay()
    {
        _sceneSetter = LoadMainScene;
        HideSceneLoader();
    }

    public void MakeSceneReady()
    {
        OnGameStart?.Invoke();
    }

    public void LoadScene() => _sceneSetter();

    private void LoadMenuScene() => SceneManager.LoadSceneAsync("Menu");

    private void LoadMainScene() => SceneManager.LoadSceneAsync("Main");

    private void HideSceneLoader() => animator.SetTrigger("Hide");
}
