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

    public void AddPoints(int pointsToAdd)
    {
        value += pointsToAdd;
        OnScoreUpdated.Invoke();
    }

    void OnValidate()
    {
        value = EditorApplication.isPlaying ? Mathf.Max(value, 0) : 0;
    }
}