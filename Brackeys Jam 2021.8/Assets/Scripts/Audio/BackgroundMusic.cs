using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] AudioSource backgroundAudio;

    private const float MUTE_DURATION = 1f;

    private void OnEnable() => PlayerHealth.OnGameOver += MuteBackgroundMusic;

    private void OnDisable() => PlayerHealth.OnGameOver -= MuteBackgroundMusic;

    private void MuteBackgroundMusic() => DOTween.To(() => backgroundAudio.volume, x => backgroundAudio.volume = x, 0, MUTE_DURATION);
}
