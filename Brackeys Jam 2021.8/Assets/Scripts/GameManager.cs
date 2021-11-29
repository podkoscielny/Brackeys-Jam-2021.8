using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] PoopType[] poopLevels;
    [SerializeField] ChaosStar[] chaosStars;

    public static event Action<int> OnScoreUpdated;
    public static event Action<int> OnChaosStarGained;
    public static event Action OnLifeSpawn;

    public bool IsGameOver { get; private set; } = false;
    public int Score { get; private set; }
    public int PoopChargeLevel { get; private set; } = 1;
    public int ChaosStarsAmount { get; private set; } = 0;
    public int MAX_CHAOS_STARS_AMOUNT => chaosStars.Length;

    public PoopType CurrentPoop => poopLevels[PoopChargeLevel - 1];
    public ChaosStar CurrentChaosStar { get; private set; } = null;

    public int PointsToNextChaosStar
    {
        get
        {
            int index = Mathf.Min(ChaosStarsAmount, MAX_CHAOS_STARS_AMOUNT - 1);
            return chaosStars[index].pointsToReach;
        }
    }

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
}
