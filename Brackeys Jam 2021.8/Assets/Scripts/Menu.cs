using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    [SerializeField] Animator sceneAnimator;
    [SerializeField] SceneController sceneController;
    [SerializeField] TextMeshProUGUI startText;

    private bool _isSceneLoaded = false;
    private bool _isKeyPressed = false;

    void OnEnable() => SceneController.OnGameStart += ShowStartText;

    void OnDisable() => SceneController.OnGameStart -= ShowStartText;

    void Update()
    {
        if (Input.anyKey && _isSceneLoaded && !_isKeyPressed)
        {
            sceneController.GoToGameplay();
            _isKeyPressed = true;
        }
    }

    private void ShowStartText()
    {
        _isSceneLoaded = true;
        startText.gameObject.SetActive(true);
    }
}
