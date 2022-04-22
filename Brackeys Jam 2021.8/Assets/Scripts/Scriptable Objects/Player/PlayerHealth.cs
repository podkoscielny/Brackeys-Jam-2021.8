using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "PlayerHealth", menuName = "ScriptableObjects/PlayerHealth")]
public class PlayerHealth : ScriptableObject
{
    public static event Action OnHealthChanged;
    public static event Action OnGameOver;

    [SerializeField] int maxHealth;
    [SerializeField] float healthValue;
    [SerializeField] float initialHealth;

    public int MAX_HEALTH => maxHealth;
    public float HealthValue => healthValue;

    void OnEnable()
    {
        ResetHealth();

        SceneController.OnSceneChange += ResetHealth;

#if UNITY_EDITOR
        EditorApplication.playModeStateChanged += ResetValuesOnEditorQuit;
#endif
    }

    void OnDisable()
    {
        ResetHealth();

        SceneController.OnSceneChange -= ResetHealth;

#if UNITY_EDITOR
        EditorApplication.playModeStateChanged -= ResetValuesOnEditorQuit;
#endif
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        maxHealth = Mathf.Max(maxHealth, 0);
        healthValue = Mathf.Clamp(healthValue, 0, maxHealth);

        if (EditorApplication.isPlaying)
        {
            OnHealthChanged?.Invoke();
        }
        else
        {
            initialHealth = healthValue;
        }

        if (EditorApplication.isPlaying && healthValue == 0) OnGameOver?.Invoke();
    }
#endif

    public void Heal(float healAmount = 1)
    {
        healthValue = Mathf.Min(healthValue + healAmount, maxHealth);
        OnHealthChanged?.Invoke();
    }

    public void TakeDamage(float damageAmount)
    {
        healthValue = Mathf.Max(0, healthValue - damageAmount);
        OnHealthChanged?.Invoke();

        if (healthValue <= 0)
            OnGameOver?.Invoke();
    }

    private void ResetHealth() => healthValue = initialHealth;

#if UNITY_EDITOR
    private void ResetValuesOnEditorQuit(PlayModeStateChange changedState)
    {
        if (changedState == PlayModeStateChange.ExitingPlayMode)
        {
            ResetHealth();
        }
    }
#endif
}
