using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerHealth", menuName = "ScriptableObjects/PlayerHealth")]
public class PlayerHealth : ScriptableObject
{
    public static event Action OnHealthChanged;
    public static event Action OnGameOver;

    [SerializeField] int maxHealth;
    [SerializeField] float healthValue;

    public int MAX_HEALTH => maxHealth;
    public float HealthValue => healthValue;

    private float _intialHealth;

    void Awake() => _intialHealth = healthValue;

    void OnDisable() => healthValue = _intialHealth;

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
}