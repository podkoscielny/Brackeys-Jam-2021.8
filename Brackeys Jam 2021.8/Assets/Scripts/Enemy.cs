using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemyController
{
    [SerializeField] HumanCharacter[] humanCharacters;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Animator enemyAnimator;

    private GameManager _gameManager;
    private bool _isHit = false;
    private const float MOVEMENT_SPEED = 4f;

    void Awake() => _gameManager = GameManager.Instance;

    void OnEnable()
    {
        SetRandomSprite();
        _isHit = false;
    }

    public void Move() => transform.position += transform.right * MOVEMENT_SPEED * Time.deltaTime;

    void SetRandomSprite()
    {
        if (humanCharacters.Length < 1) return;

        int characterIndex = Random.Range(0, humanCharacters.Length);

        spriteRenderer.sprite = humanCharacters[characterIndex].sprite;
        enemyAnimator.runtimeAnimatorController = humanCharacters[characterIndex].animatorController;
    }

    public bool HandlePoopHit()
    {
        if (!_isHit)
        {
            _gameManager.UpdateScore();
            _isHit = true;
            enemyAnimator.SetTrigger("IsHit");

            return false;
        }

        return true;
    }
}
