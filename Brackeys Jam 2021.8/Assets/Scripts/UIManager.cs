using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    void OnEnable() => GameManager.OnScoreUpdate += UpdateScore;

    void OnDisable() => GameManager.OnScoreUpdate -= UpdateScore;

    void UpdateScore(int score) => scoreText.text = score.ToString();
}
