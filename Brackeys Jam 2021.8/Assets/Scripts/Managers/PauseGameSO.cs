using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PauseGameSO", menuName = "ScriptableObjects/PauseGameSO")]
public class PauseGameSO : ScriptableObject
{
    public static event Action OnGamePaused;
    public static event Action OnGameResumed;

    public void Pause()
    {
        Time.timeScale = 0;
        OnGamePaused?.Invoke();
    }

    public void Resume()
    {
        Time.timeScale = 1;
        OnGameResumed?.Invoke();
    }
}
