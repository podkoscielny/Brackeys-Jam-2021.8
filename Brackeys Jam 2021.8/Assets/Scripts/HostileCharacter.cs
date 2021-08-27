using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

public class HostileCharacter : MonoBehaviour
{
    //[SerializeField] HumanCharacter[] humanCharacters;
    //[SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Transform firePoint;
    [SerializeField] Animator enemyAnimator;
    [SerializeField] GameObject splashEffect;

    private ObjectPooler _objectPooler;
    private GameManager _gameManager;
    private float _movementSpeed = 4f;
    private float _minPositionX = -7f;
    private float _maxPositionX = 7f;
    private bool _hasReachedTarget = false;
    private Vector2 _randomStopPosition;

    void OnEnable()
    {
        splashEffect.SetActive(false);
        //SetRandomSprite();
    }

    void OnDisable() => CancelInvoke(nameof(Shoot));

    void Start()
    {
        _objectPooler = ObjectPooler.Instance;
        _gameManager = GameManager.Instance;

        float randomPositionX = Random.Range(_minPositionX, _maxPositionX);
        _randomStopPosition = new Vector2(randomPositionX, transform.position.y);
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _randomStopPosition, _movementSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, _randomStopPosition) < 0.01f && !_hasReachedTarget)
        {
            _hasReachedTarget = true;
            InvokeRepeating(nameof(ShootAnimation), 0f, 2f);
        }
    }

    void ShootAnimation() => enemyAnimator.SetTrigger("Shoot");

    void Shoot()
    {
        GameObject bullet = _objectPooler.GetFromPoolInActive(Tags.Bullet);
        bullet.transform.position = firePoint.position;
        bullet.SetActive(true);
    }

    //void SetRandomSprite()
    //{
    //    if (humanCharacters.Length < 1) return;

    //    int characterIndex = Random.Range(0, humanCharacters.Length);

    //    spriteRenderer.sprite = humanCharacters[characterIndex].sprite;
    //    enemyAnimator.runtimeAnimatorController = humanCharacters[characterIndex].animatorController;
    //}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.Poop))
        {
            splashEffect.SetActive(true);
            _objectPooler.AddToPool(Tags.Poop, collision.gameObject);
            _gameManager.UpdateScore();
        }
    }
}
