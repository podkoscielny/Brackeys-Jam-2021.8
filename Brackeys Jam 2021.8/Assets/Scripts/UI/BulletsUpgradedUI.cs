using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsUpgradedUI : MonoBehaviour
{
    [SerializeField] Animator bulletsUpgradedAnimator;
    [SerializeField] AudioSource bulletsUpgradedAudio;

    private void OnEnable() => PoopSystem.OnPoopUpgrade += ShowUpgradedBulletsText;

    private void OnDisable() => PoopSystem.OnPoopUpgrade -= ShowUpgradedBulletsText;

    private void ShowUpgradedBulletsText()
    {
        bulletsUpgradedAudio.Play();
        bulletsUpgradedAnimator.SetTrigger("Appear");
    }
}
