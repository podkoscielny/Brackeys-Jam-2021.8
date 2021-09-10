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
    private Vector2 _explosionRedOffset = new Vector2(0f, 0.25f);
    private Vector2 _explosionGreenOffset = new Vector2(0f, 0.75f);
    private int _gravityScale = 3;
    private bool _isFalling = false;

    void OnEnable()
    {
        _isFalling = false;
        poopRb.gravityScale = 0;
    }

    void Start()
    {
        _gameManager = GameManager.Instance;
        _objectPooler = ObjectPooler.Instance;
        _spawnPoop = GameObject.FindGameObjectWithTag("SpawnPoop");
    }

    void LateUpdate()
    {
        if (!_isFalling)
        {
            transform.position = _spawnPoop.transform.position;
        }
    }

    void SetGravity()
    {
        poopRb.gravityScale = _gravityScale;
        _isFalling = true;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.Ground) && _gameManager.PoopChargeLevel >= 4)
        {
            GameObject explosion = _objectPooler.GetFromPoolInActive(Tags.Explosion);
            explosion.transform.position = (Vector2)transform.position + _explosionGreenOffset;
            explosion.SetActive(true);
        }
    }
}
