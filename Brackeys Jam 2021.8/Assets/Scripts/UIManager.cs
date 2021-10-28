using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject[] hearts;
    [SerializeField] Transform chaosStarsWrapper;
    [SerializeField] GameObject chaosStarPrefab;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] Animator bulletsUpgradedAnimator;
    [SerializeField] Sprite halfHeartSprite;

    private List<GameObject> chaosStars = new List<GameObject>();

    void OnEnable()
    {
        GameManager.OnScoreUpdated += UpdateScore;
        GameManager.OnChaosStarGained += EnableChaosStar;
        GameManager.OnPoopUpgrade += ShowBulletsUpgradedText;
        GameManager.OnGameOver += SetGameOverPanel;
        GameManager.OnGetHit += UpdateHeartsAmount;
    }

    void OnDisable()
    {
        GameManager.OnScoreUpdated -= UpdateScore;
        GameManager.OnChaosStarGained -= EnableChaosStar;
        GameManager.OnPoopUpgrade -= ShowBulletsUpgradedText;
        GameManager.OnGameOver -= SetGameOverPanel;
        GameManager.OnGetHit -= UpdateHeartsAmount;
    }

    void Start() => InitializeChaosStars();

    void InitializeChaosStars()
    {
        for (int i = 0; i < GameManager.Instance.MaxChaosStarsAmount; i++)
        {
            GameObject star = Instantiate(chaosStarPrefab);
            star.transform.SetParent(chaosStarsWrapper);
            chaosStars.Add(star);
        }
    }

    void UpdateScore(int score) => scoreText.text = score.ToString();

    void EnableChaosStar(int chaosStarsAmount) => chaosStars[chaosStarsAmount - 1].SetActive(true);

    void ShowBulletsUpgradedText() => bulletsUpgradedAnimator.SetTrigger("Appear");

    void SetGameOverPanel() => gameOverPanel.SetActive(true);

    void UpdateHeartsAmount(float playersLives)
    {
        int index;

        for (float i = 0; i < hearts.Length; i += 0.5f)
        {
            if(i % 1 == 0 && i + 1 < playersLives)
            {
                index = (int)i;
                hearts[index].SetActive(true);
            }
            
            else if(i % 1 == 0 && i >= playersLives)
            {
                index = (int)i;
                hearts[index].SetActive(false);
            }
            else if (i % 1 != 0 && i == playersLives)
            {
                index = (int)(i - 0.5f);
                hearts[index].GetComponent<Image>().sprite = halfHeartSprite;
            }
        }
    }
}
