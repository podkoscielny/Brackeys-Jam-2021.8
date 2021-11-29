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

    public int MAX_HEALTH => maxHealth;
    public float HealthValue => healthValue;

    private float _initialHealth;
    private int _initialMaxHealth;

    void Awake()
    {
        _initialHealth = healthValue;
        _initialMaxHealth = maxHealth;
    }

    void OnEnable()
    {
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

    void OnValidate()
    {
        maxHealth = EditorApplication.isPlaying ? _initialMaxHealth : Mathf.Max(maxHealth, 0);
        healthValue = Mathf.Clamp(healthValue, 0, maxHealth);

        if (EditorApplication.isPlaying)
        {
            OnHealthChanged?.Invoke();
        }
        else
        {
            _initialHealth = healthValue;
            _initialMaxHealth = maxHealth;
        }

        if (EditorApplication.isPlaying && healthValue == 0) OnGameOver?.Invoke();
    }

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

    private void ResetHealth()
    {
        healthValue = _initialHealth;
        maxHealth = _initialMaxHealth;
    }

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