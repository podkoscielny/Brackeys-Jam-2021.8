using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D bulletRb;

    //private GameObject _player;
    private float _speed = 6f;

    private void OnEnable()
    {
        //if (_player == null) _player = GameObject.FindGameObjectWithTag("Player");
        GoTowardsTarget();
    }

    public void SetBulletRotation(Quaternion rotation) => transform.rotation = rotation;

    void GoTowardsTarget()
    {
        //Vector2 direction = _player.transform.position - transform.position;
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //bulletRb.rotation = angle;
        bulletRb.AddForce(transform.right * _speed, ForceMode2D.Impulse);
    }
}
