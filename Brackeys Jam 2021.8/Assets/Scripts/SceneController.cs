using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static Action OnGameStart;

    private Animator animator;

    void Start() => animator = GetComponent<Animator>();

    public void Restart()
    {
        animator.SetTrigger("Hide");
        StartCoroutine(RestartGame());
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(1f);

        SceneManager.LoadSceneAsync("Main");
    }

    public void GoToMenu() => SceneManager.LoadSceneAsync("Menu");

    public void GoToGameplay()
    {
        SceneManager.LoadSceneAsync("Main");
    }

    public void MakeSceneReady()
    {
        OnGameStart?.Invoke();
    }
}
