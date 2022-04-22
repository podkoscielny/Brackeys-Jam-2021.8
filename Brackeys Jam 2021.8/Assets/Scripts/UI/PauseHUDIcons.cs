using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseHUDIcons : MonoBehaviour
{
    [SerializeField] GameObject pauseIcon;
    [SerializeField] GameObject resumeIcon;

    private void OnEnable()
    {
        PauseGameSO.OnGamePaused += EnableResumeIcon;
        PauseGameSO.OnGameResumed += EnablePauseIcon;
    }

    private void OnDisable()
    {
        PauseGameSO.OnGamePaused -= EnableResumeIcon;
        PauseGameSO.OnGameResumed -= EnablePauseIcon;
    }

    private void EnablePauseIcon()
    {
        pauseIcon.SetActive(true);
        resumeIcon.SetActive(false);
    }

    private void EnableResumeIcon()
    {
        resumeIcon.SetActive(true);
        pauseIcon.SetActive(false);
    }
}
