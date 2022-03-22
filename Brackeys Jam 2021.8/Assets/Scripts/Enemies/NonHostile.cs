using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonHostile : MonoBehaviour, IEnemyMovement
{
    [SerializeField] HumanCharacter[] humanCharacters;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Animator enemyAnimator;

    private const float MOVEMENT_SPEED = 4f;

    void OnEnable() => SetRandomSprite();

    public void Move() => transform.position += MOVEMENT_SPEED * Time.deltaTime * transform.right;

    private void SetRandomSprite()
    {
        if (humanCharacters.Length < 1) return;

        int characterIndex = Random.Range(0, humanCharacters.Length);

        spriteRenderer.sprite = humanCharacters[characterIndex].Sprite;
        enemyAnimator.runtimeAnimatorController = humanCharacters[characterIndex].AnimatorController;
    }
}
