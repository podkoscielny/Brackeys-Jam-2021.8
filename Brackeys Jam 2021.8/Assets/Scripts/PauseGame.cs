using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] GameObject pauseText;

    private GameManager _gameManager;
    private bool _isGamePaused = false;
    private bool _canBePaused = false;

    void OnEnable() => SceneController.OnGameStart += AllowGamePause;

    void OnDisable() => SceneController.OnGameStart -= AllowGamePause;

    void Start() => _gameManager = GameManager.Instance;

    void Update()
    {
        if (!_gameManager.IsGameOver && _canBePaused && Input.GetButtonDown("Pause"))
        {
            Pause();
        }
    }

    private void AllowGamePause() => _canBePaused = true;

    private void Pause()
    {
        Time.timeScale = _isGamePaused ? 1 : 0;
        pauseText.SetActive(!_isGamePaused);

        _isGamePaused = !_isGamePaused;
    }
}
