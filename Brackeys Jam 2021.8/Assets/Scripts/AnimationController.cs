using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour, IControlAnimation
{
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D playerRb;

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
        playerRb.velocity = new Vector2(0, playerRb.velocity.y);
    }

    public void OnFalling()
    {

    }

    public void OnCrouching(bool isCrouching)
    {

    }

    public void OnJumping()
    {
        animator.SetBool("IsJumping", true);
    }

    public void OnDoubleJump()
    {

    }

    public void OnPickup()
    {
        animator.SetTrigger("Pickup");
    }
}
