using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tags = TagSystem.Tags;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController2D controller;
    [SerializeField] Animator playerAnimator;
    [SerializeField] Transform poopSpawn;
    [SerializeField] Joystick joystick;
    [SerializeField] ObjectPool objectPool;

    private WaitForSeconds _waitForShootDelay;
    private WaitForSeconds _waitForMovementDelay;

    private bool _canShoot = true;
    private bool _canMove = false;
    private float _horizontalMovement = 0f;

    private const float JOYSTICK_SENSITIVITY = 0.2f;
    private const float MOVEMENT_SPEED = 5f;
    private const float SHOOT_DELAY = 0.5f;
    private const float MOVEMENT_DELAY = 0.7f;

    private delegate void MovementDelegate();
    private MovementDelegate _movementDelegate;

    private void OnEnable()
    {
        SceneController.OnGameStart += EnableMovement;
        PlayerCollisions.OnHitTaken += DisableMovement;
    }

    private void OnDisable()
    {
        SceneController.OnGameStart -= EnableMovement;
        PlayerCollisions.OnHitTaken -= DisableMovement;
    }

    private void Start()
    {
        CacheWaitForSeconds();
        _movementDelegate = StandaloneMovement;

#if UNITY_ANDROID
        _movementDelegate = MobileMovement;
#endif
    }

    private void Update() => _movementDelegate();

    private void FixedUpdate()
    {
        if (!_canMove) return;

        controller.Move(_horizontalMovement);
    }

    private void StandaloneMovement()
    {
        if (!_canMove) return;

        _horizontalMovement = Input.GetAxisRaw("Horizontal") * MOVEMENT_SPEED;

        playerAnimator.SetFloat("Speed", Mathf.Abs(_horizontalMovement));

        if (Input.GetButtonDown("Jump")) controller.Jump();

        if (Input.GetButtonDown("Fire1")) Shoot();
    }

    private void MobileMovement()
    {
        _horizontalMovement = Mathf.Abs(joystick.HorizontalPosition) > JOYSTICK_SENSITIVITY ? joystick.Horizontal * MOVEMENT_SPEED : 0;

        playerAnimator.SetFloat("Speed", Mathf.Abs(_horizontalMovement));
    }

    public void Shoot()
    {
        if (!_canShoot) return;

        GameObject poop = objectPool.GetFromPool(Tags.Poop);
        poop.transform.position = poopSpawn.position;
        _canShoot = false;

        StartCoroutine(DelayShooting());
    }

    private IEnumerator DelayShooting()
    {
        yield return _waitForShootDelay;

        _canShoot = true;
    }

    private void DisableMovement() => StartCoroutine(BlockMovement());

    private void EnableMovement() => _canMove = true;

    private IEnumerator BlockMovement()
    {
        _canMove = false;

        yield return _waitForMovementDelay;

        _canMove = true;
    }

    private void CacheWaitForSeconds()
    {
        _waitForShootDelay = new WaitForSeconds(SHOOT_DELAY);
        _waitForMovementDelay = new WaitForSeconds(MOVEMENT_DELAY);
    }
}
