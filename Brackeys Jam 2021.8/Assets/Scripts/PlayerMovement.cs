using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController2D controller;
    [SerializeField] Rigidbody2D playerRb;
    [SerializeField] Animator playerAnimator;
    [SerializeField] GameObject poopPrefab;
    [SerializeField] Transform poopSpawn;

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

        if (Input.GetButtonDown("Fire1"))
        {
            GameObject poop = Instantiate(poopPrefab, poopSpawn.position, poopSpawn.rotation);
            Rigidbody2D poopRb = poop.GetComponent<Rigidbody2D>();

            poopRb.velocity = playerRb.velocity / 2;
        }
    }

    void FixedUpdate() => controller.Move(_horizontalMovement);
}
