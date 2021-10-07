using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    [SerializeField] Animator sceneAnimator;
    [SerializeField] SceneController controller;
    [SerializeField] TextMeshProUGUI startText;

    private bool _isSceneLoaded = false;

    private void OnEnable() => SceneController.OnGameStart += ShowStartText;

    void OnDisable() => SceneController.OnGameStart -= ShowStartText;

    void Update()
    {
        if (Input.anyKey && _isSceneLoaded)
        {
            StartGame();
        }
    }

    void ShowStartText()
    {
        _isSceneLoaded = true;
        startText.gameObject.SetActive(true);
    }

    void StartGame()
    {
        sceneAnimator.SetTrigger("Hide");
        startText.gameObject.SetActive(false);
    }
}
