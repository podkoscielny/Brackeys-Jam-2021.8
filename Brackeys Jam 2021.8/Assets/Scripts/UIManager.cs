using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject[] chaosStars;
    [SerializeField] Animator bulletsUpgradedAnimator;

    void OnEnable()
    {
        GameManager.OnScoreUpdated += UpdateScore;
        GameManager.OnChaosStarGained += EnableChaosStar;
        GameManager.OnPoopUpgrade += ShowBulletsUpgradedText;
    }

    void OnDisable()
    {
        GameManager.OnScoreUpdated -= UpdateScore;
        GameManager.OnChaosStarGained -= EnableChaosStar;
        GameManager.OnPoopUpgrade -= ShowBulletsUpgradedText;
    }

    void UpdateScore(int score) => scoreText.text = score.ToString();

    void EnableChaosStar(int chaosStarsAmount) => chaosStars[chaosStarsAmount - 1].SetActive(true);

    void ShowBulletsUpgradedText() => bulletsUpgradedAnimator.SetTrigger("Appear");
}
