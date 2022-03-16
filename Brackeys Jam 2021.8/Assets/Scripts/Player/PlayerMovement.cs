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

    private bool _canShoot = true;
    private bool _canMove = true;
    private float _horizontalMovement = 0f;

    private const float JOYSTICK_SENSITIVITY = 0.4f;
    private const float MOVEMENT_SPEED = 5f;
    private const float SHOOT_DELAY = 0.5f;

    private delegate void MovementDelegate();
    private MovementDelegate _movementDelegate;

    void Start()
    {
        _waitForShootDelay = new WaitForSeconds(SHOOT_DELAY);
        _movementDelegate = StandaloneMovement;

#if UNITY_ANDROID
        _movementDelegate = MobileMovement;
#endif
    }

    void Update() => _movementDelegate();

    void FixedUpdate()
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

    public void DisableMovement() => _canMove = false;

    public void EnableMovement() => _canMove = true;

    public void Shoot()
    {
        if (!_canShoot) return;

        GameObject poop = objectPool.GetFromPool(Tags.Poop);
        poop.transform.position = poopSpawn.position;
        _canShoot = false;

        StartCoroutine(DelayShooting());
    }

    IEnumerator DelayShooting()
    {
        yield return _waitForShootDelay;

        _canShoot = true;
    }
}
