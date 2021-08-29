using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public bool IsReady { get; private set; } = false;

    void GoToGameplay()
    {
        SceneManager.LoadSceneAsync("Main");
    }

    void MakeSceneReady() => IsReady = true;
}
