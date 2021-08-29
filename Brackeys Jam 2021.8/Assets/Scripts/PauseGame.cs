using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] GameObject pauseText;

    private bool _isGamePaused = false;

    void Update()
    {
        if (!GameManager.Instance.IsGameOver && ( Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)))
        {
            if (_isGamePaused)
            {
                Time.timeScale = 1;
                pauseText.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                pauseText.SetActive(true);
            }

            _isGamePaused = !_isGamePaused;
        }
    }
}
