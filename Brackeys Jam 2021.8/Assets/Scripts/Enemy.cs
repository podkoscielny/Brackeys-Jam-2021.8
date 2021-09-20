using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

public class Enemy : MonoBehaviour
{
    [SerializeField] HumanCharacter[] humanCharacters;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Animator enemyAnimator;
    [SerializeField] EnemyCharacter baseFunctionalityController;

    private ObjectPooler _objectPooler;
    private GameManager _gameManager;
    private float _movementSpeed = 4f;
    private bool _isHit = false;
    private Quaternion _rightRotation = new Quaternion(0, 0, 0, 1);
    private Quaternion _leftRotation = new Quaternion(0, 1, 0, 0);

    void Awake()
    {
        _objectPooler = ObjectPooler.Instance;
        _gameManager = GameManager.Instance;
    }

    void OnEnable()
    {
        SetRandomSprite();
        _isHit = false;
    }

    void Update()
    {
        if (baseFunctionalityController.CanMove)
            Move();
    }

    void Move() => transform.position += transform.right * _movementSpeed * Time.deltaTime;

    public void SetFacing(bool isFacingRight)
    {
        if (isFacingRight)
        {
            transform.rotation = _rightRotation;
        }
        else
        {
            transform.rotation = _leftRotation;
        }
    }

    void SetRandomSprite()
    {
        if (humanCharacters.Length < 1) return;

        int characterIndex = Random.Range(0, humanCharacters.Length);

        spriteRenderer.sprite = humanCharacters[characterIndex].sprite;
        enemyAnimator.runtimeAnimatorController = humanCharacters[characterIndex].animatorController;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.Poop) && _gameManager.PoopChargeLevel < 4)
        {

            baseFunctionalityController.SetSplashEffect(collision.transform.position);
            _objectPooler.AddToPool(Tags.Poop, collision.gameObject);

            if (!_isHit)
            {
                _gameManager.UpdateScore();
                _isHit = true;
                baseFunctionalityController.DisableMoving();
                enemyAnimator.SetTrigger("IsHit");
            }
        }
    }
}
