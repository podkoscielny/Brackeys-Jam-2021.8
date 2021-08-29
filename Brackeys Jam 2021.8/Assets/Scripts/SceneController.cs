using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public bool IsReady { get; private set; } = false;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void GoToGameplay()
    {
        SceneManager.LoadSceneAsync("Main");
    }

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

    void MakeSceneReady() => IsReady = true;
}
