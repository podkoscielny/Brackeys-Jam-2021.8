using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsUpgradedUI : MonoBehaviour
{
    [SerializeField] Animator bulletsUpgradedAnimator;

    private void OnEnable() => PoopSystem.OnPoopUpgrade += ShowUpgradedBulletsText;

    private void OnDisable() => PoopSystem.OnPoopUpgrade -= ShowUpgradedBulletsText;

    private void ShowUpgradedBulletsText() => bulletsUpgradedAnimator.SetTrigger("Appear");
}