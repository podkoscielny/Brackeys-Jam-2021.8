using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cage : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] SpriteRenderer cageRenderer;

    private Color _cageColor = Color.white;
    private Vector3 _impactRotation = new Vector3(0, 0, 5f);

    private const float FALL_DURATION = 0.2f;
    private const float ROTATION_DURATION = 0.4f;
    private const float OFFSET_FROM_PLAYER = 0.55f;

    private void OnEnable() => PlayerHealth.OnGameOver += MoveToPlayersPosition;

    private void OnDisable() => PlayerHealth.OnGameOver -= MoveToPlayersPosition;

    private void Start() => SetCageToTransparent();

    private void MoveToPlayersPosition()
    {
        SetCageToOpaque();

        float targetYPosition = player.position.y + OFFSET_FROM_PLAYER;

        transform.DOMoveY(targetYPosition, FALL_DURATION).SetEase(Ease.InCirc).OnComplete(() => transform.DOPunchRotation(_impactRotation, ROTATION_DURATION));
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
