using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] LayerMask interactableMask;
    [SerializeField] AnimationController animationController;

    private GameManager _gameManager;
    private bool _isInteracting = false;
    private const float INTERACTION_RADIUS = 0.7f;

    void Start()
    {
        _gameManager = GameManager.Instance;
        Physics2D.IgnoreLayerCollision(9, 8);
    }

    void Update()
    {
        if (Input.GetButtonDown("Interaction") && !_isInteracting && !_gameManager.IsGameOver)
        {
            InteractWithItem();
        }
    }

    void InteractWithItem()
    {
        Collider2D interactableObject = Physics2D.OverlapCircle(transform.position, INTERACTION_RADIUS, interactableMask);

        if (interactableObject != null)
        {
            interactableObject.GetComponent<IInteractable>()?.PickUp();
            _isInteracting = true;
            animationController.OnPickup();
        }
    }

    public void EnableInteracting() => _isInteracting = false; // Enable Interacting when animation is over
}
