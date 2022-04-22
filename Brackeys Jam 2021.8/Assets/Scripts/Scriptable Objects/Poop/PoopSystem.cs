using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "PoopSystem", menuName = "ScriptableObjects/PoopSystem")]
public class PoopSystem : ScriptableObject
{
    public static event Action<float> OnCornEaten;
    public static event Action OnPoopUpgrade;

    [SerializeField] int poopChargeLevel = 1;
    [SerializeField] PoopType[] poopLevels;

    public int PoopChargeLevel => poopChargeLevel;
    public int ChargeGoal => poopChargeLevel * 3;
    public PoopType CurrentPoop => poopLevels[poopChargeLevel - 1];
    public int MAX_POOP_CHARGE_LEVEL => poopLevels.Length;

    public int PointsToNextPoopLevel
    {
        get
        {
            int index = Mathf.Min(PoopChargeLevel, MAX_POOP_CHARGE_LEVEL - 1);
            return poopLevels[index].PointsToReach;
        }
    }

    private int _cornEaten = 0;

    void OnEnable()
    {
        SceneController.OnSceneChange += ResetPoopSystem;

#if UNITY_EDITOR
        EditorApplication.playModeStateChanged += ResetValuesOnEditorQuit;
#endif
    }

    void OnDisable()
    {
        ResetPoopSystem();

        SceneController.OnSceneChange -= ResetPoopSystem;

#if UNITY_EDITOR
        EditorApplication.playModeStateChanged -= ResetValuesOnEditorQuit;
#endif
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        poopChargeLevel = poopLevels.Length > 0 ? Mathf.Clamp(poopChargeLevel, 1, poopLevels.Length) : 1;

        if (EditorApplication.isPlaying)
        {
            OnPoopUpgrade?.Invoke();
        }
    }
#endif

    public void EatCorn()
    {
        _cornEaten++;

        float fillAmount = (float)_cornEaten / (float)ChargeGoal;
        OnCornEaten?.Invoke(fillAmount);

        if (_cornEaten == ChargeGoal && poopChargeLevel < MAX_POOP_CHARGE_LEVEL)
        {
            UpgradePoop();
        }
    }

    private void UpgradePoop()
    {
        poopChargeLevel++;
        _cornEaten = 0;

        OnPoopUpgrade?.Invoke();
    }

    private void ResetPoopSystem()
    {
        _cornEaten = 0;
        poopChargeLevel = 1;
    }

#if UNITY_EDITOR
    private void ResetValuesOnEditorQuit(PlayModeStateChange changedState)
    {
        if (changedState == PlayModeStateChange.ExitingPlayMode)
        {
            ResetPoopSystem();
        }
    }
#endif
}