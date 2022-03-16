using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Score score;

    private void OnEnable() => Score.OnScoreUpdated += UpdateScore;

    private void OnDisable() => Score.OnScoreUpdated -= UpdateScore;

    private void UpdateScore() => scoreText.text = score.Value.ToString();
}