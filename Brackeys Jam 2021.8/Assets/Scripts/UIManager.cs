using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject mobileUI;
    [SerializeField] Animator bulletsUpgradedAnimator;
    [SerializeField] Slider poopLevelSlider;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI currentPoopLevelText;
    [SerializeField] TextMeshProUGUI nextPoopLevelText;
    [SerializeField] PoopSystem poopSystem;
    [SerializeField] Score score;

    void OnEnable()
    {
        Score.OnScoreUpdated += UpdateScore;
        PoopSystem.OnPoopUpgrade += UpdatePoopLevelUI;
        PlayerHealth.OnGameOver += SetGameOverPanel;
        PoopSystem.OnCornEaten += UpdateFillAmount;
    }

    void OnDisable()
    {
        Score.OnScoreUpdated -= UpdateScore;
        PoopSystem.OnPoopUpgrade -= UpdatePoopLevelUI;
        PlayerHealth.OnGameOver -= SetGameOverPanel;
        PoopSystem.OnCornEaten -= UpdateFillAmount;
    }

    void Start() => DisplayMobileUI();

    private void DisplayMobileUI()
    {
        bool shouldMobileUIBeDisplayed = false;

#if UNITY_ANDROID
        shouldMobileUIBeDisplayed = true;
#endif

        mobileUI.SetActive(shouldMobileUIBeDisplayed);
    }

    private void UpdateScore() => scoreText.text = score.Value.ToString();

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
