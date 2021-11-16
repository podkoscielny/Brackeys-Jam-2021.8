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
    [SerializeField] Joystick joystick;

    private ObjectPooler _objectPooler;
    private bool _canShoot = true;
    private bool _canMove = true;
    private float _horizontalMovement = 0f;

    private const float MOVEMENT_SPEED = 5f;
    private const float SHOOT_DELAY = 0.5f;

    private delegate void MovementDelegate();
    private MovementDelegate _movementDelegate;

    #if UNITY_ANDROID
    private Vector2 _fingerDownPosition;
    private Vector2 _fingerUpPosition;
    #endif

    void Start()
    {
        _objectPooler = ObjectPooler.Instance;
        _movementDelegate = StandaloneMovement;

        #if UNITY_ANDROID
        _movementDelegate = MobileMovement;
        #endif
    }

    void Update()
    {
        _movementDelegate();
    }

    void FixedUpdate()
    {
        if (_canMove)
            controller.Move(_horizontalMovement);
    }

    private void StandaloneMovement()
    {
        _horizontalMovement = Input.GetAxisRaw("Horizontal") * MOVEMENT_SPEED;

        playerAnimator.SetFloat("Speed", Mathf.Abs(_horizontalMovement));

        if (Input.GetButtonDown("Jump"))
        {
            controller.Jump();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void MobileMovement()
    {
        _horizontalMovement = joystick.Horizontal * MOVEMENT_SPEED;

        playerAnimator.SetFloat("Speed", Mathf.Abs(_horizontalMovement));

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                _fingerUpPosition = touch.position;
                _fingerDownPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                _fingerDownPosition = touch.position;

                if (VerticalMoveValue() > 20 && VerticalMoveValue() > HorizontalMoveValue())
                {
                    controller.Jump();
                }
            }
        }
    }

    private float VerticalMoveValue() => Mathf.Abs(_fingerDownPosition.y - _fingerUpPosition.y);

    private float HorizontalMoveValue() => Mathf.Abs(_fingerDownPosition.x - _fingerUpPosition.x);

    public void MobileJump() => controller.Jump();

    public void ResetMovement() => _horizontalMovement = 0;

    public void DisableMovement() => _canMove = false;

    public void EnableMovement() => _canMove = true;

    public void Shoot()
    {
        if (_canShoot)
        {
            GameObject poop = _objectPooler.GetFromPool(Tags.Poop);
            poop.transform.position = poopSpawn.position;

            _canShoot = false;
            StartCoroutine(DelayShooting());
        }
    }

    IEnumerator DelayShooting()
    {
        yield return new WaitForSeconds(SHOOT_DELAY);

        _canShoot = true;
    }
}
