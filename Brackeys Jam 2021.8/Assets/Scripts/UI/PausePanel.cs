using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    [SerializeField] PauseGame pauseGame;

    private void OnEnable()
    {
        PauseGame.OnGamePaused += EnablePausePanel;
        PauseGame.OnGameResumed += DisablePausePanel;
    }

    private void OnDisable()
    {
        PauseGame.OnGamePaused -= EnablePausePanel;
        PauseGame.OnGameResumed -= DisablePausePanel;
    }

#if UNITY_STANDALONE
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) pauseGame.Pause();
    }
#endif

    private void EnablePausePanel() => pausePanel.SetActive(true);

    private void DisablePausePanel() => pausePanel.SetActive(false);
}
