using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] LayerMask interactableMask;
    [SerializeField] Animator playerAnimator;
    [SerializeField] AudioSource pickablesAudio;

    private const float INTERACTION_RADIUS = 0.7f;

#if !UNITY_ANDROID
    void Update()
    {
        if (Input.GetButtonDown("Interaction")) InteractWithItem();
    }

#endif

    public void InteractWithItem()
    {
        Collider2D interactableObject = Physics2D.OverlapCircle(transform.position, INTERACTION_RADIUS, interactableMask);

        if (interactableObject != null && interactableObject.TryGetComponent(out Interactable interactable))
        {
            pickablesAudio.clip = interactable.SoundEffect;
            pickablesAudio.Play();

            interactable.PickUp();

            playerAnimator.SetTrigger("Pickup");
        }
    }
}
