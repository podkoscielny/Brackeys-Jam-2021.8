using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour, IControlAnimation
{
    [SerializeField] Animator animator;

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
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

    public void OnPickup()
    {
        animator.SetTrigger("Pickup");
    }
}
