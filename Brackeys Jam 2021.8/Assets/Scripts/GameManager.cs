using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action<int> OnScoreUpdated;
    public static event Action<int> OnChaosStarGained;
    public static event Action<float> OnGetHit;
    public static event Action OnPoopUpgrade;
    public static event Action OnGameOver;

    [SerializeField] PoopType[] poopLevels;
    [SerializeField] ChaosStar[] chaosStars;

    public bool IsGameOver { get; private set; } = false;
    public int Score { get; private set; }
    public int PoopChargeLevel { get; private set; } = 1;
    public int ChargeGoal { get; private set; } = 3;
    public int ChaosStarsAmount { get; private set; } = 0;
    public int MaxChaosStarsAmount { get { return chaosStars.Length; } }
    public float PlayersLives { get; private set; } = 3;
    public ExplosionType ExplosionEffect { get; private set; }
    public PoopType CurrentPoop { get { return poopLevels[PoopChargeLevel - 1]; } }
    public ChaosStar CurrentChaosStar
    {
        get
        {
            int index = Mathf.Min(ChaosStarsAmount, MaxChaosStarsAmount - 1);
            return chaosStars[index];
        }
    }

    private int _cornEaten = 0;

    private int MAX_POOP_CHARGE_LEVEL { get { return poopLevels.Length; } }

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
        Score += CurrentPoop.pointsWorth;

        if (Score >= CurrentChaosStar.pointsToReach && ChaosStarsAmount < MaxChaosStarsAmount)
        {
            ChaosStarsAmount++;
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

    public bool CanCornBeSpawn() => Score > CurrentPoop.pointsWorth * 3 && PoopChargeLevel < MAX_POOP_CHARGE_LEVEL;

    void UpgradePoop()
    {
        PoopChargeLevel++;

        ChargeGoal = PoopChargeLevel * 3;
        _cornEaten = 0;

        OnPoopUpgrade?.Invoke();
    }
}
