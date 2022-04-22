using System;
using UnityEngine;

public class AnimationController : MonoBehaviour, IControlAnimation
{
    public static event Action OnGameOverLanded;

    [Header("Player components")]
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D playerRb;

    [Header("Audio")]
    [SerializeField] AudioSource playerAudio;
    [SerializeField] AudioClip jumpSound;

    private bool _isGameOver = false;

    private void OnEnable() => PlayerHealth.OnGameOver += SetGameToOver;

    private void OnDisable() => PlayerHealth.OnGameOver -= SetGameToOver;

    public void OnLanding()
    {
        if (_isGameOver) OnGameOverLanded?.Invoke();

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
        PlayJumpSound();
    }

    public void OnDoubleJump()
    {
        animator.SetTrigger("IsJumping");
        PlayJumpSound();
    }

    private void SetGameToOver()
    {
        _isGameOver = true;
        animator.SetTrigger("GameOver");
    }

    private void PlayJumpSound()
    {
        playerAudio.clip = jumpSound;
        playerAudio.Play();
    }
}
