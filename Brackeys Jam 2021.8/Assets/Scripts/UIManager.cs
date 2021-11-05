using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Transform heartsWrapper;
    [SerializeField] Transform chaosStarsWrapper;
    [SerializeField] GameObject heartPrefab;
    [SerializeField] GameObject chaosStarPrefab;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] Animator bulletsUpgradedAnimator;
    [SerializeField] Slider poopLevelSlider;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI currentPoopLevelText;
    [SerializeField] TextMeshProUGUI nextPoopLevelText;

    private GameManager _gameManager;
    private List<GameObject> _hearts;
    private List<GameObject> _chaosStars;

    void OnEnable()
    {
        GameManager.OnScoreUpdated += UpdateScore;
        GameManager.OnChaosStarGained += EnableChaosStar;
        GameManager.OnPoopUpgrade += UpdatePoopLevelUI;
        GameManager.OnGameOver += SetGameOverPanel;
        GameManager.OnUpdateHeartsAmount += UpdateHeartsAmount;
        GameManager.OnCornEaten += UpdateFillAmount;
    }

    void OnDisable()
    {
        GameManager.OnScoreUpdated -= UpdateScore;
        GameManager.OnChaosStarGained -= EnableChaosStar;
        GameManager.OnPoopUpgrade -= UpdatePoopLevelUI;
        GameManager.OnGameOver -= SetGameOverPanel;
        GameManager.OnUpdateHeartsAmount -= UpdateHeartsAmount;
        GameManager.OnCornEaten -= UpdateFillAmount;
    }

    void Start()
    {
        _gameManager = GameManager.Instance;
        _hearts = new List<GameObject>();
        _chaosStars = new List<GameObject>();

        InitializeChaosStars();
        InitializeLives();
        UpdateHeartsAmount(_gameManager.PlayersLives);
    }

    private void InitializeChaosStars()
    {
        for (int i = 0; i < _gameManager.MAX_CHAOS_STARS_AMOUNT; i++)
        {
            GameObject star = Instantiate(chaosStarPrefab);
            star.transform.SetParent(chaosStarsWrapper);
            _chaosStars.Add(star);
        }
    }

    private void InitializeLives()
    {
        for (int i = 0; i < _gameManager.MAX_LIVES_AMOUNT; i++)
        {
            GameObject life = Instantiate(heartPrefab);
            life.transform.SetParent(heartsWrapper);
            _hearts.Add(life);
        }
    }

    private void UpdateScore(int score) => scoreText.text = score.ToString();

    private void EnableChaosStar(int chaosStarsAmount) => _chaosStars[chaosStarsAmount - 1].SetActive(true);

    private void SetGameOverPanel() => gameOverPanel.SetActive(true);

    private void UpdateFillAmount(float fillAmount) => poopLevelSlider.value = fillAmount;

    private void UpdatePoopLevelUI()
    {
        bulletsUpgradedAnimator.SetTrigger("Appear");

        bool isPoopMaxed = _gameManager.PoopChargeLevel == _gameManager.MAX_POOP_CHARGE_LEVEL;

        string nextText = isPoopMaxed ? "Max" : $"{_gameManager.PoopChargeLevel + 1}";
        float fillAmount = !isPoopMaxed ? 0 : 1;

        UpdateFillAmount(fillAmount);
        currentPoopLevelText.text = $"{_gameManager.PoopChargeLevel}";
        nextPoopLevelText.text = $"{nextText}";
    }

    private void UpdateHeartsAmount(float playersLives)
    {
        for (int i = 0; i < _hearts.Count; i++)
        {
            _hearts[i].GetComponent<Image>().fillAmount = i <= playersLives - 1 ? 1f : 0f; // change to iamges instead of gameobjects
        }

        if (playersLives % 1 != 0)
        {
            int notFullHeartIndex = Mathf.FloorToInt(playersLives);
            _hearts[notFullHeartIndex].GetComponent<Image>().fillAmount = playersLives - notFullHeartIndex;
        }
    }
}
