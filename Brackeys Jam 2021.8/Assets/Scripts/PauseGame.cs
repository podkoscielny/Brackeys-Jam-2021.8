using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] GameObject pauseText;

    private GameManager _gameManager;
    private bool _isGamePaused = false;

    void Start() => _gameManager = GameManager.Instance;

    void Update()
    {
        if (!_gameManager.IsGameOver && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)))
        {
            Pause();
        }
    }

    void Pause()
    {
        Time.timeScale = _isGamePaused ? 1 : 0;
        pauseText.SetActive(!_isGamePaused);

        _isGamePaused = !_isGamePaused;
    }
}
