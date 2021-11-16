using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] LayerMask interactableMask;
    [SerializeField] Animator playerAnimator;

    private const float INTERACTION_RADIUS = 0.7f;

#if !UNITY_ANDROID
    void Update()
    {
        if (Input.GetButtonDown("Interaction"))
        {
            InteractWithItem();
        }
    }

#endif

    public void InteractWithItem()
    {
        Collider2D interactableObject = Physics2D.OverlapCircle(transform.position, INTERACTION_RADIUS, interactableMask);

        if (interactableObject != null)
        {
            interactableObject.GetComponent<IInteractable>()?.PickUp();
            playerAnimator.SetTrigger("Pickup");
        }
    }
}
