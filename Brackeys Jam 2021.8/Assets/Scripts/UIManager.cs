using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject[] chaosStars;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] Animator bulletsUpgradedAnimator;

    void OnEnable()
    {
        GameManager.OnScoreUpdated += UpdateScore;
        GameManager.OnChaosStarGained += EnableChaosStar;
        GameManager.OnPoopUpgrade += ShowBulletsUpgradedText;
        GameManager.OnGameOver += SetGameOverPanel;
    }

    void OnDisable()
    {
        GameManager.OnScoreUpdated -= UpdateScore;
        GameManager.OnChaosStarGained -= EnableChaosStar;
        GameManager.OnPoopUpgrade -= ShowBulletsUpgradedText;
        GameManager.OnGameOver -= SetGameOverPanel;
    }

    void UpdateScore(int score) => scoreText.text = score.ToString();

    void EnableChaosStar(int chaosStarsAmount) => chaosStars[chaosStarsAmount - 1].SetActive(true);

    void ShowBulletsUpgradedText() => bulletsUpgradedAnimator.SetTrigger("Appear");

    void SetGameOverPanel() => gameOverPanel.SetActive(true);
}
