using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    private GameManager _gameManager;

    void OnEnable() => Enemy.OnEnemyHit += UpdateScore;

    void OnDisable() => Enemy.OnEnemyHit -= UpdateScore;

    void Start() => _gameManager = GameManager.Instance;

    void UpdateScore(int score) => scoreText.text = score.ToString();
}
