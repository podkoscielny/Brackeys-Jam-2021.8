using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action<int> OnScoreUpdated;
    public static event Action<int> OnChaosStarGained;

    public int Score { get; private set; }

    private int _cornEaten = 0;
    private int _poopChargeLevel = 1;
    private int _chargeGoal = 5;
    private int _chaosStarsAmount = 0;
    private int _chaosStarGoal = 100;
    private int _scoreAmount = 10;

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

        if(Score >= _chaosStarGoal)
        {
            _chaosStarsAmount++;
            _chaosStarGoal *= _chaosStarsAmount * 2;
            OnChaosStarGained?.Invoke(_chaosStarsAmount);
        }

        OnScoreUpdated?.Invoke(Score);
    }

    public void EatCorn()
    {
        _cornEaten++;

        if(_cornEaten == _chargeGoal)
        {
            _poopChargeLevel++;
            _chargeGoal = _poopChargeLevel * 5;
            _cornEaten = 0;
            _scoreAmount = 10 * _poopChargeLevel * _poopChargeLevel;
        }
    }
}
