using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController2D controller;
    [SerializeField] Rigidbody2D playerRb;
    [SerializeField] Animator playerAnimator;
    [SerializeField] GameObject poopPrefab;
    [SerializeField] Transform poopSpawn;

    private ObjectPooler _objectPooler;
    private bool _canShoot = true;
    private float _horizontalMovement = 0f;
    private float _movementSpeed = 200f;
    private const float _shootDelay = 0.5f;

    void OnEnable() => GameManager.OnGameOver += DisableSelf;

    void OnDisable() => GameManager.OnGameOver -= DisableSelf;

    void Start() => _objectPooler = ObjectPooler.Instance;

    void Update()
    {
        if (GameManager.Instance.IsGameOver) return;

        _horizontalMovement = Input.GetAxisRaw("Horizontal") * _movementSpeed;
        playerAnimator.SetFloat("Speed", Mathf.Abs(_horizontalMovement));

        if (Input.GetButtonDown("Jump"))
        {
            controller.Jump();
        }

        if (Input.GetButtonDown("Fire1") && _canShoot)
        {
            //GameObject poop = Instantiate(poopPrefab, poopSpawn.position, poopSpawn.rotation);
            //Rigidbody2D poopRb = poop.GetComponent<Rigidbody2D>();

            //poopRb.velocity = playerRb.velocity / 2;
            Shoot();
        }
    }

    void FixedUpdate() => controller.Move(_horizontalMovement);

    void Shoot()
    {
        GameObject poop = _objectPooler.GetFromPool(Tags.Poop);
        poop.transform.position = poopSpawn.position;

        _canShoot = false;
        StartCoroutine(DelayShooting());
    }

    IEnumerator DelayShooting()
    {
        yield return new WaitForSeconds(_shootDelay);

        _canShoot = true;
    }

    void DisableSelf() => this.enabled = false;
}
