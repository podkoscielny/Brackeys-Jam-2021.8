using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int Score { get; private set; }

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

    void OnEnable() => Enemy.OnEnemyHit += UpdateScore;

    void OnDisable() => Enemy.OnEnemyHit -= UpdateScore;

    void UpdateScore(int score) => Score = score;
}
