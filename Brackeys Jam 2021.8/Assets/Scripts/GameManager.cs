using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] PoopType[] poopLevels;
    [SerializeField] ChaosStar[] chaosStars;

    public static event Action<int> OnScoreUpdated;
    public static event Action<int> OnChaosStarGained;
    public static event Action<float> OnCornEaten;
    public static event Action<float> OnUpdateHeartsAmount;
    public static event Action OnPoopUpgrade;
    public static event Action OnGameOver;
    public static event Action OnLifeSpawn;

    public bool IsGameOver { get; private set; } = false;
    public int Score { get; private set; }
    public int PoopChargeLevel { get; private set; } = 1;
    public int ChargeGoal { get; private set; } = 3;
    public int ChaosStarsAmount { get; private set; } = 0;
    public int MAX_CHAOS_STARS_AMOUNT { get { return chaosStars.Length; } }
    public int MAX_POOP_CHARGE_LEVEL { get { return poopLevels.Length; } }

    public float PlayersLives { get; private set; } = 3;
    public int MAX_LIVES_AMOUNT { get; private set; } = 5;

    public ExplosionType ExplosionEffect { get; private set; }
    public PoopType CurrentPoop { get { return poopLevels[PoopChargeLevel - 1]; } }
    public ChaosStar CurrentChaosStar { get; private set; } = null;

    public int PointsToNextChaosStar
    {
        get
        {
            int index = Mathf.Min(ChaosStarsAmount, MAX_CHAOS_STARS_AMOUNT - 1);
            return chaosStars[index].pointsToReach;
        }
    }

    private int _cornEaten = 0;
    private int _lifeSpawnGoal = 3000;

    private const int SPAWN_LIFE_TARGET = 3000;

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
        Score += CurrentPoop.pointsWorth;

        if (Score >= PointsToNextChaosStar && ChaosStarsAmount < MAX_CHAOS_STARS_AMOUNT)
        {
            CurrentChaosStar = chaosStars[ChaosStarsAmount];
            ChaosStarsAmount++;
            OnChaosStarGained?.Invoke(ChaosStarsAmount);
        }

        if (Score >= _lifeSpawnGoal)
        {
            OnLifeSpawn?.Invoke();
            _lifeSpawnGoal += SPAWN_LIFE_TARGET;
        }

        OnScoreUpdated?.Invoke(Score);
    }

    public void EatCorn()
    {
        _cornEaten++;

        float fillAmount = (float)_cornEaten / (float)ChargeGoal;
        OnCornEaten?.Invoke(fillAmount);

        if (_cornEaten == ChargeGoal && PoopChargeLevel < MAX_POOP_CHARGE_LEVEL)
        {
            UpgradePoop();
        }
    }

    public void Heal(float healAmount = 1)
    {
        PlayersLives = Mathf.Min(PlayersLives + healAmount, MAX_LIVES_AMOUNT);
        OnUpdateHeartsAmount?.Invoke(PlayersLives);
    }

    public void GetHit(float damageAmount)
    {
        PlayersLives = Mathf.Max(0, PlayersLives - damageAmount);
        OnUpdateHeartsAmount?.Invoke(PlayersLives);

        if (PlayersLives <= 0)
            GameOver();
    }

    public void GameOver()
    {
        IsGameOver = true;
        OnGameOver?.Invoke();
    }

    public bool CanCornBeSpawn() => Score > CurrentPoop.pointsWorth * 3 && PoopChargeLevel < MAX_POOP_CHARGE_LEVEL;

    private void UpgradePoop()
    {
        PoopChargeLevel++;

        ChargeGoal = PoopChargeLevel * 3;
        _cornEaten = 0;

        OnPoopUpgrade?.Invoke();
    }
}
