using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonHostile : MonoBehaviour, IEnemyMovement
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Animator enemyAnimator;
    [SerializeField] ChaosStarsSystem chaosStarsSystem;
    [SerializeField] HumanCharacter[] humanCharacters;

    private float _movementSpeed = 4f;

    private void OnEnable()
    {
        SetRandomSprite();
        SetEnemySpeed();
    }

    public void Move() => transform.position += _movementSpeed * Time.deltaTime * transform.right;

    private void SetRandomSprite()
    {
        if (humanCharacters.Length < 1) return;

        int characterIndex = Random.Range(0, humanCharacters.Length);

        spriteRenderer.sprite = humanCharacters[characterIndex].Sprite;
        enemyAnimator.runtimeAnimatorController = humanCharacters[characterIndex].AnimatorController;
    }

    private void SetEnemySpeed() => _movementSpeed = chaosStarsSystem.CurrentChaosStar.LastEnemyPicked.MovementSpeed;
}
