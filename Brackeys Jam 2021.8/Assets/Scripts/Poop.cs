using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

public class Poop : MonoBehaviour
{
    [SerializeField] Rigidbody2D poopRb;

    private ObjectPooler _objectPooler;
    private GameManager _gameManager;
    private GameObject _spawnPoop;
    private Vector2 _explosionOffset = new Vector2(0f, 0.75f);
    private bool _isFalling = false;
    private const int GRAVITY_SCALE = 3;

    void Awake()
    {
        _gameManager = GameManager.Instance;
        _objectPooler = ObjectPooler.Instance;
        _spawnPoop = GameObject.FindGameObjectWithTag("SpawnPoop");
    }

    void OnEnable()
    {
        _isFalling = false;
        poopRb.gravityScale = 0;
    }

    void LateUpdate()
    {
        if (!_isFalling)
        {
            transform.position = _spawnPoop.transform.position;
        }
    }

    public void SetGravity() // Invoke after animation
    {
        poopRb.gravityScale = GRAVITY_SCALE;
        _isFalling = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.Ground) && _gameManager.PoopChargeLevel >= 4)
        {
            GameObject explosion = _objectPooler.GetFromPoolInActive(Tags.Explosion);
            explosion.transform.position = (Vector2)transform.position + _explosionOffset;
            explosion.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
