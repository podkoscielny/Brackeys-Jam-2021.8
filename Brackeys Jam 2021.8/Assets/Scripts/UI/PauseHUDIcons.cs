using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseHUDIcons : MonoBehaviour
{
    [SerializeField] GameObject pauseIcon;
    [SerializeField] GameObject resumeIcon;

    private void OnEnable()
    {
        PauseGame.OnGamePaused += EnableResumeIcon;
        PauseGame.OnGameResumed += EnablePauseIcon;
    }

    private void OnDisable()
    {
        PauseGame.OnGamePaused -= EnableResumeIcon;
        PauseGame.OnGameResumed -= EnablePauseIcon;
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
