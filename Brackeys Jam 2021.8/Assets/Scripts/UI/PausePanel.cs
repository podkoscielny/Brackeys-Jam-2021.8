using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;

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

    private void EnablePausePanel() => pausePanel.SetActive(true);

    private void DisablePausePanel() => pausePanel.SetActive(false);
}
