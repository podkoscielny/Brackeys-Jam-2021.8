using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poop : MonoBehaviour
{
    [SerializeField] Rigidbody2D poopRb;

    private GameObject _spawnPoop;
    private int _gravityScale = 3;
    private bool _isFalling = false;

    void OnEnable()
    {
        _isFalling = false;
        poopRb.gravityScale = 0;
    }

    void Start()
    {
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
}
