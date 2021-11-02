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
    [SerializeField] TextMeshProUGUI currentPoopLevelText;
    [SerializeField] TextMeshProUGUI nextPoopLevelText;
    [SerializeField] Slider poopLevelSlider;

    private GameManager _gameManager;
    private List<GameObject> chaosStars = new List<GameObject>();

    void OnEnable()
    {
        GameManager.OnScoreUpdated += UpdateScore;
        GameManager.OnChaosStarGained += EnableChaosStar;
        GameManager.OnPoopUpgrade += UpdatePoopLevelUI;
        GameManager.OnGameOver += SetGameOverPanel;
        GameManager.OnGetHit += UpdateHeartsAmount;
        GameManager.OnCornEaten += UpdateFillAmount;
    }

    void OnDisable()
    {
        GameManager.OnScoreUpdated -= UpdateScore;
        GameManager.OnChaosStarGained -= EnableChaosStar;
        GameManager.OnPoopUpgrade -= UpdatePoopLevelUI;
        GameManager.OnGameOver -= SetGameOverPanel;
        GameManager.OnGetHit -= UpdateHeartsAmount;
        GameManager.OnCornEaten -= UpdateFillAmount;
    }

    void Start()
    {
        _gameManager = GameManager.Instance;
        InitializeChaosStars();
    }

    void InitializeChaosStars()
    {
        for (int i = 0; i < _gameManager.MaxChaosStarsAmount; i++)
        {
            GameObject star = Instantiate(chaosStarPrefab);
            star.transform.SetParent(chaosStarsWrapper);
            chaosStars.Add(star);
        }
    }

    void UpdateScore(int score) => scoreText.text = score.ToString();

    void EnableChaosStar(int chaosStarsAmount) => chaosStars[chaosStarsAmount - 1].SetActive(true);

    void SetGameOverPanel() => gameOverPanel.SetActive(true);

    void UpdateFillAmount(float fillAmount) => poopLevelSlider.value = fillAmount;

    void UpdatePoopLevelUI()
    {
        bulletsUpgradedAnimator.SetTrigger("Appear");

        bool isPoopMaxed = _gameManager.PoopChargeLevel == _gameManager.MAX_POOP_CHARGE_LEVEL;

        string nextText = isPoopMaxed ? "Max" : $"{_gameManager.PoopChargeLevel + 1}";
        float fillAmount = !isPoopMaxed ? 0 : 1;

        UpdateFillAmount(fillAmount);
        currentPoopLevelText.text = $"{_gameManager.PoopChargeLevel}";
        nextPoopLevelText.text = $"{nextText}";
    }

    void UpdateHeartsAmount(float playersLives)
    {
        int index;

        for (float i = 0; i < hearts.Length; i += 0.5f)
        {
            if (i % 1 == 0 && i + 1 < playersLives)
            {
                index = (int)i;
                hearts[index].SetActive(true);
            }

            else if (i % 1 == 0 && i >= playersLives)
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
