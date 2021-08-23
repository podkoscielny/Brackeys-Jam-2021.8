using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController2D controller;
    [SerializeField] Animator playerAnimator;

    private float _horizontalMovement = 0f;
    private float _movementSpeed = 200f;

    void Update()
    {
        _horizontalMovement = Input.GetAxisRaw("Horizontal") * _movementSpeed;
        playerAnimator.SetFloat("Speed", Mathf.Abs(_horizontalMovement));

        if (Input.GetButtonDown("Jump"))
        {
            controller.Jump();
        }
    }

    void FixedUpdate() => controller.Move(_horizontalMovement);
}
