using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    [SerializeField] PauseGame pauseGame;

    private float _previousMasterVolume;

    private const float TURNED_DOWN_VOLUME = 0.3f;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) pauseGame.Pause();
    }

    private void EnablePausePanel()
    {
        _previousMasterVolume = AudioListener.volume;
        MasterVolume.TurnVolumeDown(TURNED_DOWN_VOLUME);

        pausePanel.SetActive(true);
    }

    private void DisablePausePanel()
    {
        MasterVolume.TurnVolumeUp(_previousMasterVolume);

        pausePanel.SetActive(false);
    }
}
