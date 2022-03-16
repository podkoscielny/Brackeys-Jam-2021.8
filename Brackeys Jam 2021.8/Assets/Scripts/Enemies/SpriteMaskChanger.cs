using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMaskChanger : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] SpriteMask spriteMask;

    void Update()
    {
        if(spriteRenderer.sprite != spriteMask.sprite)
        {
            spriteMask.sprite = spriteRenderer.sprite;
        }
    }
}
