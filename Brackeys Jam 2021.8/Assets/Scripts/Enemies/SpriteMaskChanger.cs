using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMaskChanger : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] SpriteMask spriteMask;

    private void Update() => SetMaskSprite();

    private void SetMaskSprite() => spriteMask.sprite = spriteRenderer.sprite;
}
