using System;
using UnityEngine;

public class AnimationController : MonoBehaviour, IControlAnimation
{
    public static event Action OnLanded;

    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D playerRb;

    private bool _isGameOver = false;

    private void OnEnable() => PlayerHealth.OnGameOver += SetGameToOver;

    private void OnDisable() => PlayerHealth.OnGameOver -= SetGameToOver;

    public void OnLanding()
    {
        if (_isGameOver) OnLanded?.Invoke();

        animator.SetBool("IsGrounded", true);
        playerRb.velocity = new Vector2(0, playerRb.velocity.y);
    }

    public void OnFalling()
    {
        animator.SetBool("IsGrounded", false);
    }

    public void OnCrouching(bool isCrouching)
    {

    }

    public void OnJumping()
    {
        animator.SetTrigger("IsJumping");
        animator.SetBool("IsGrounded", false);
    }

    public void OnDoubleJump()
    {
        animator.SetTrigger("IsJumping");
    }

    private void SetGameToOver()
    {
        _isGameOver = true;
        animator.SetTrigger("GameOver");
    }
}
