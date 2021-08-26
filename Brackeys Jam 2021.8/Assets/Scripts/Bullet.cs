using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D bulletRb;

    private Transform _player;
    private float _speed = 6f;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        Vector2 direction = _player.position - transform.position;

        bulletRb.AddForce(direction * _speed, ForceMode2D.Impulse);
    }
}
