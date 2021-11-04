using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] LayerMask interactableMask;
    [SerializeField] AnimationController animationController;

    private const float INTERACTION_RADIUS = 0.7f;

    void Update()
    {
        if (Input.GetButtonDown("Interaction"))
        {
            InteractWithItem();
        }
    }

    private void InteractWithItem()
    {
        Collider2D interactableObject = Physics2D.OverlapCircle(transform.position, INTERACTION_RADIUS, interactableMask);

        if (interactableObject != null)
        {
            interactableObject.GetComponent<IInteractable>()?.PickUp();
            animationController.OnPickup();
        }
    }
}
