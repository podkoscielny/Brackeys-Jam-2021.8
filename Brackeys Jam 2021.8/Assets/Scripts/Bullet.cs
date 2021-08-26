using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D bulletRb;

    private GameObject _player;
    private float _speed = 6f;

    private void OnEnable()
    {
        if (_player == null) _player = GameObject.FindGameObjectWithTag("Player");
        GoTowardsTarget();
    }

    void GoTowardsTarget()
    {
        Vector2 direction = _player.transform.position - transform.position;
        bulletRb.AddForce(direction * _speed, ForceMode2D.Impulse);
    }
}
