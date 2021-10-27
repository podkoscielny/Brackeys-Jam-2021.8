using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action<int> OnScoreUpdated;
    public static event Action<int> OnChaosStarGained;
    public static event Action<float> OnGetHit;
    public static event Action OnPoopUpgrade;
    public static event Action OnGameOver;

    public int Score { get; private set; }
    public float PlayersLives { get; private set; } = 3;
    public int PoopChargeLevel { get; private set; } = 1;
    public int ChargeGoal { get; private set; } = 3;
    public int ChaosStarsAmount { get; private set; } = 0;
    public bool IsGameOver { get; private set; } = false;
    public ExplosionType ExplosionEffect { get; private set; }
    public PoopType CurrentPoop { get; private set; }

    [SerializeField] PoopType[] poopLevels;

    private int _cornEaten = 0;
    private int _chaosStarGoal = 10;
    private int _scoreAmount = 0;

    private int MAX_POOP_CHARGE_LEVEL;
    public readonly int MIN_EXPLOSION_POOP_LEVEL = 4;
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

    void Start()
    {
        CurrentPoop = poopLevels[0];
        _scoreAmount = CurrentPoop.pointsWorth;
        MAX_POOP_CHARGE_LEVEL = poopLevels.Length;
    }

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

        if (_cornEaten == ChargeGoal && PoopChargeLevel < MAX_POOP_CHARGE_LEVEL)
        {
            UpgradePoop();
        }
    }

    public void GetHit(float damageAmount)
    {
        PlayersLives -= damageAmount;
        OnGetHit?.Invoke(PlayersLives);

        if (PlayersLives <= 0)
            GameOver();
    }

    public void GameOver()
    {
        IsGameOver = true;
        OnGameOver?.Invoke();
    }

    public bool CanPoopBeSpawn() => Score > _scoreAmount * 3 && PoopChargeLevel < MAX_POOP_CHARGE_LEVEL;

    void UpgradePoop()
    {
        PoopChargeLevel++;

        CurrentPoop = poopLevels[PoopChargeLevel - 1];

        ChargeGoal = PoopChargeLevel * 3;
        _cornEaten = 0;
        _scoreAmount = CurrentPoop.pointsWorth;

        OnPoopUpgrade?.Invoke();
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
