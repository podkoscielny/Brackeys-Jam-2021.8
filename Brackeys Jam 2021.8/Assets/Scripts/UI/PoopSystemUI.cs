using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PoopSystemUI : MonoBehaviour
{
    [SerializeField] Slider poopLevelSlider;
    [SerializeField] PoopSystem poopSystem;
    [SerializeField] Animator bulletsUpgradedAnimator;
    [SerializeField] TextMeshProUGUI currentPoopLevelText;
    [SerializeField] TextMeshProUGUI nextPoopLevelText;

    private void OnEnable()
    {
        PoopSystem.OnPoopUpgrade += UpdatePoopLevelUI;
        PoopSystem.OnCornEaten += UpdateFillAmount;
    }

    private void OnDisable()
    {
        PoopSystem.OnPoopUpgrade -= UpdatePoopLevelUI;
        PoopSystem.OnCornEaten -= UpdateFillAmount;
    }

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