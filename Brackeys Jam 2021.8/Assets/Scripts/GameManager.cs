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
    private int _maxPoopChargeLevel = 5;
    private int _maxChaosStarsAmount = 5;

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

        if (Score >= _chaosStarGoal && ChaosStarsAmount < _maxChaosStarsAmount)
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

        if (_cornEaten == _chargeGoal && PoopChargeLevel < _maxPoopChargeLevel)
        {
            UpgradePoop();
        }
    }

    public void GameOver()
    {
        OnGameOver?.Invoke();
    }

    void UpgradePoop()
    {
        PoopChargeLevel++;
        _chargeGoal = PoopChargeLevel * 3;
        _cornEaten = 0;
        _scoreAmount = 10 * PoopChargeLevel * PoopChargeLevel;
        OnPoopUpgrade?.Invoke();

        if(PoopChargeLevel == 4)
        {
            ExplosionEffect = explosions[0];
        }
        else if(PoopChargeLevel == 5)
        {
            ExplosionEffect = explosions[1];
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
