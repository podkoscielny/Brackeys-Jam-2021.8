using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

public class Enemy : MonoBehaviour
{
    [SerializeField] HumanCharacter[] humanCharacters;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Animator enemyAnimator;
    [SerializeField] GameObject splashEffect;
    [SerializeField] LayerMask layerToIgnoreCollision;

    private ObjectPooler _objectPooler;
    private GameManager _gameManager;
    private float _movementSpeed = 4f;
    private int _layerIgnore = 8;
    private bool _isMovingRight = true;
    private bool _isHit = false;
    private bool _canMove = true;
    private Quaternion _rightRotation = new Quaternion(0, 0, 0, 1);
    private Quaternion _leftRotation = new Quaternion(0, 1, 0, 0);

    void OnEnable()
    {
        splashEffect.SetActive(false);
        SetRandomSprite();
    }

    private void OnDisable()
    {
        _isHit = false;
    }

    void Start()
    {
        _objectPooler = ObjectPooler.Instance;
        _gameManager = GameManager.Instance;

        Physics2D.IgnoreLayerCollision(_layerIgnore, _layerIgnore);
    }

    void Update()
    {
        if (_canMove)
            Move();
    }

    void Move()
    {
        if (_isMovingRight)
        {
            transform.position += Vector3.right * _movementSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * _movementSpeed * Time.deltaTime;
        }
    }

    public void DisableMoving() => _canMove = false;

    public void SetFacing(bool isFacingRight)
    {
        _isMovingRight = isFacingRight;

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

    void EnableMoving() => _canMove = true;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.Poop) && _gameManager.PoopChargeLevel < 4)
        {

            splashEffect.transform.position = collision.transform.position;
            splashEffect.SetActive(true);
            _objectPooler.AddToPool(Tags.Poop, collision.gameObject);

            if (!_isHit)
            {
                _gameManager.UpdateScore();
                _isHit = true;
                _canMove = false;
                enemyAnimator.SetTrigger("IsHit");
            }
        }
    }
}
