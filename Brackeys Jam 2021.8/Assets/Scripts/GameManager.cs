using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action<int> OnScoreUpdated;
    public static event Action<int> OnChaosStarGained;
    public static event Action OnPoopUpgrade;
    public static event Action OnGameOver;

    public int Score { get; private set; }
    public int PoopChargeLevel { get; private set; } = 1;
    public int ChaosStarsAmount { get; private set; } = 0;
    public bool IsGameOver { get; private set; } = false;
    public ExplosionType ExplosionEffect { get; private set; }

    [SerializeField] ExplosionType[] explosions;

    private int _cornEaten = 0;
    private int _chargeGoal = 3;
    private int _chaosStarGoal = 10;
    private int _scoreAmount = 10;

    public readonly int MIN_EXPLOSION_POOP_LEVEL = 4;
    private const int MAX_POOP_CHARGE_LEVEL = 6;
    private const int MAX_CHAOS_STARS_AMOUNT = 5;

    public static GameManager Instance;

    #region Singleton
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    public void UpdateScore()
    {
        Score += _scoreAmount;

        if (Score >= _chaosStarGoal && ChaosStarsAmount < MAX_CHAOS_STARS_AMOUNT)
        {
            ChaosStarsAmount++;
            SetChaosStarsGoal();
            OnChaosStarGained?.Invoke(ChaosStarsAmount);
        }

        OnScoreUpdated?.Invoke(Score);
    }

    public void EatCorn()
    {
        _cornEaten++;

        if (_cornEaten == _chargeGoal && PoopChargeLevel < MAX_POOP_CHARGE_LEVEL)
        {
            UpgradePoop();
        }
    }

    public void GameOver() => OnGameOver?.Invoke();

    void UpgradePoop()
    {
        PoopChargeLevel++;
        _chargeGoal = PoopChargeLevel * 3;
        _cornEaten = 0;
        _scoreAmount = 10 * PoopChargeLevel * PoopChargeLevel;
        OnPoopUpgrade?.Invoke();

        for (int i = 0; i <= MAX_POOP_CHARGE_LEVEL; i++)
        {
            if (i < MIN_EXPLOSION_POOP_LEVEL) continue;

            if(i == PoopChargeLevel)
            {
                int explosionIndex = i - MIN_EXPLOSION_POOP_LEVEL;
                ExplosionEffect = explosions[explosionIndex];
            }
            else
            {
                continue;
            }
        }
    }

    void SetChaosStarsGoal()
    {
        switch (ChaosStarsAmount)
        {
            case 1:
                _chaosStarGoal = 200;
                break;

            case 2:
                _chaosStarGoal = 400;
                break;

            case 3:
                _chaosStarGoal = 800;
                break;

            case 4:
                _chaosStarGoal = 1600;
                break;

            case 5:
                _chaosStarGoal = 3000;
                break;

            default:
                break;
        }
    }
}
