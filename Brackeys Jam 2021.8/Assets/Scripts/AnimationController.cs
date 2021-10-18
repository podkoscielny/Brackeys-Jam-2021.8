using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour, IControlAnimation
{
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D playerRb;

    public void OnLanding()
    {
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

    public void OnPickup()
    {
        animator.SetTrigger("Pickup");
    }
}
