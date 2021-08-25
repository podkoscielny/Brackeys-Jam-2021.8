using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action<int> OnScoreUpdate;

    public int Score { get; private set; }

    private int _cornEaten = 0;
    private int _poopChargeLevel = 1;
    private int _chargeGoal = 5;
    private int _chaosStarsAmount = 0;
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
        OnScoreUpdate?.Invoke(Score);
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
