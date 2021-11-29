using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Transform chaosStarsWrapper;
    [SerializeField] GameObject chaosStarPrefab;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject mobileUI;
    [SerializeField] Animator bulletsUpgradedAnimator;
    [SerializeField] Slider poopLevelSlider;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI currentPoopLevelText;
    [SerializeField] TextMeshProUGUI nextPoopLevelText;
    [SerializeField] PoopSystem poopSystem;

    private GameManager _gameManager;
    private List<GameObject> _chaosStars;

    void OnEnable()
    {
        GameManager.OnScoreUpdated += UpdateScore;
        GameManager.OnChaosStarGained += EnableChaosStar;
        PoopSystem.OnPoopUpgrade += UpdatePoopLevelUI;
        PlayerHealth.OnGameOver += SetGameOverPanel;
        PoopSystem.OnCornEaten += UpdateFillAmount;
    }

    void OnDisable()
    {
        GameManager.OnScoreUpdated -= UpdateScore;
        GameManager.OnChaosStarGained -= EnableChaosStar;
        PoopSystem.OnPoopUpgrade -= UpdatePoopLevelUI;
        PlayerHealth.OnGameOver -= SetGameOverPanel;
        PoopSystem.OnCornEaten -= UpdateFillAmount;
    }

    void Start()
    {
        _gameManager = GameManager.Instance;
        _chaosStars = new List<GameObject>();

        InitializeChaosStars();

        #if UNITY_ANDROID
        DisplayMobileUI();
        #endif
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

    private void DisplayMobileUI() => mobileUI.SetActive(true);

    private void UpdateScore(int score) => scoreText.text = score.ToString();

    private void EnableChaosStar(int chaosStarsAmount) => _chaosStars[chaosStarsAmount - 1].SetActive(true);

    private void SetGameOverPanel() => gameOverPanel.SetActive(true);

    private void UpdateFillAmount(float fillAmount) => poopLevelSlider.value = fillAmount;

    private void UpdatePoopLevelUI()
    {
        int poopLevel = poopSystem.PoopChargeLevel;

        bulletsUpgradedAnimator.SetTrigger("Appear");

        bool isPoopMaxed = poopLevel == poopSystem.MAX_POOP_CHARGE_LEVEL;

        string nextText = isPoopMaxed ? "Max" : $"{poopLevel + 1}";
        float fillAmount = !isPoopMaxed ? 0 : 1;

        UpdateFillAmount(fillAmount);
        currentPoopLevelText.text = $"{poopLevel}";
        nextPoopLevelText.text = $"{nextText}";
    }
}
