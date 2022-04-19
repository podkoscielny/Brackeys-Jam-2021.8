using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cage : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] Transform player;
    [SerializeField] SpriteRenderer cageRenderer;
    [SerializeField] AudioSource cageAudio;

    private Color _cageColor = Color.white;
    private Vector3 _impactRotation = new Vector3(0, 0, 5f);

    private const float FALL_DURATION = 0.2f;
    private const float SPAWN_OFFSET = 2.3f;
    private const float ROTATION_DURATION = 0.4f;
    private const float OFFSET_FROM_PLAYER = 0.55f;

    private void OnEnable() => AnimationController.OnLanded += MoveToPlayersPosition;

    private void OnDisable() => AnimationController.OnLanded -= MoveToPlayersPosition;

    private void Start() => SetCageToTransparent();

    private void MoveToPlayersPosition()
    {
        SetCageToOpaque();
        SpawnCageOutsideCameraView();

        float targetYPosition = player.position.y + OFFSET_FROM_PLAYER;

        transform.DOMoveY(targetYPosition, FALL_DURATION).SetEase(Ease.InCirc).OnComplete(PunchRotateCage);
    }

    private void PunchRotateCage()
    {
        cageAudio.Play();
        transform.DOPunchRotation(_impactRotation, ROTATION_DURATION);
    }

    private void SpawnCageOutsideCameraView()
    {
        Vector3 spawnPosition = mainCamera.ViewportToWorldPoint(new Vector3(0, 1));

        spawnPosition.x = player.position.x;
        spawnPosition.y += SPAWN_OFFSET;
        spawnPosition.z = transform.position.z;

        transform.position = spawnPosition;
    }

    private void SetCageToOpaque()
    {
        _cageColor.a = 1;
        cageRenderer.color = _cageColor;
    }

    private void SetCageToTransparent()
    {
        _cageColor.a = 0;
        cageRenderer.color = _cageColor;
    }
}
