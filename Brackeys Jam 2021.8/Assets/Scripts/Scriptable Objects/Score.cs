using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "Score", menuName = "ScriptableObjects/Score")]
public class Score : ScriptableObject
{
    public static event Action OnScoreUpdated;

    [SerializeField] int value;

    public int Value => value;

    private void OnEnable()
    {
        SceneController.OnSceneChange += ResetScore;


#if UNITY_EDITOR
        EditorApplication.playModeStateChanged += ResetValuesOnEditorQuit;
#endif
    }

    private void OnDisable()
    {
        ResetScore();

        SceneController.OnSceneChange -= ResetScore;

#if UNITY_EDITOR
        EditorApplication.playModeStateChanged -= ResetValuesOnEditorQuit;
#endif
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if(EditorApplication.isPlaying)
        {
            value = Mathf.Max(value, 0);
            OnScoreUpdated?.Invoke();
        }
        else
        {
            value = 0;
        }
    }
#endif

    public void AddPoints(int pointsToAdd)
    {
        value += pointsToAdd;
        OnScoreUpdated.Invoke();
    }

#if UNITY_EDITOR
    private void ResetValuesOnEditorQuit(PlayModeStateChange changedState)
    {
        if (changedState == PlayModeStateChange.ExitingPlayMode)
        {
            ResetScore();
        }
    }
#endif

    private void ResetScore() => value = 0;
}