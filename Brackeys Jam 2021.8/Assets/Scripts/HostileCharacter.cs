using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

public class HostileCharacter : MonoBehaviour
{
    //[SerializeField] HumanCharacter[] humanCharacters;
    //[SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Animator enemyAnimator;

    private ObjectPooler _objectPooler;
    private GameManager _gameManager;
    private float _movementSpeed = 4f;
    private float _minPositionX = -7f;
    private float _maxPositionX = 7f;
    private Vector2 _randomStopPosition;

    //void OnEnable() => SetRandomSprite();

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
        
        if(Vector2.Distance(transform.position, _randomStopPosition) < 0.01f)
        {
            enemyAnimator.SetFloat("Speed", 0f);
        }    
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
            _objectPooler.AddToPool(Tags.Poop, collision.gameObject);
            _gameManager.UpdateScore();
        }
    }
}
