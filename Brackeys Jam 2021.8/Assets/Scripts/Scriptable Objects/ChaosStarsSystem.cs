using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "ChaosStarsSystem", menuName = "ScriptableObjects/ChaosStarsSystem")]
public class ChaosStarsSystem : ScriptableObject
{
    public static event Action OnChaosStarGained;

    [SerializeField] Score score;
    [SerializeField] ChaosStar[] chaosStars;

    public int ChaosStarsAmount { get; private set; } = 0;
    public int MAX_CHAOS_STARS_AMOUNT => chaosStars.Length;

    public ChaosStar CurrentChaosStar { get; private set; } = null;

    public int PointsToNextChaosStar
    {
        get
        {
            int index = Mathf.Min(ChaosStarsAmount, MAX_CHAOS_STARS_AMOUNT - 1);
            return chaosStars[index].pointsToReach;
        }
    }

    void OnEnable()
    {
        Score.OnScoreUpdated += CheckScoreToUpdateChaosStarsAmount;
        SceneController.OnSceneChange += ResetValues;

#if UNITY_EDITOR
        EditorApplication.playModeStateChanged += ResetValuesOnEditorQuit;
#endif
    }

    void OnDisable()
    {
        ResetValues();

        Score.OnScoreUpdated -= CheckScoreToUpdateChaosStarsAmount;
        SceneController.OnSceneChange -= ResetValues;

#if UNITY_EDITOR
        EditorApplication.playModeStateChanged -= ResetValuesOnEditorQuit;
#endif
    }

    private void CheckScoreToUpdateChaosStarsAmount()
    {
        if (score.Value >= PointsToNextChaosStar && ChaosStarsAmount < MAX_CHAOS_STARS_AMOUNT)
        {
            CurrentChaosStar = chaosStars[ChaosStarsAmount];
            ChaosStarsAmount++;
            OnChaosStarGained?.Invoke();
        }
    }

#if UNITY_EDITOR
    private void ResetValuesOnEditorQuit(PlayModeStateChange changedState)
    {
        if (changedState == PlayModeStateChange.ExitingPlayMode)
        {
            ResetValues();
        }
    }
#endif

    private void ResetValues()
    {
        ChaosStarsAmount = 0;
        CurrentChaosStar = null;
    }
}