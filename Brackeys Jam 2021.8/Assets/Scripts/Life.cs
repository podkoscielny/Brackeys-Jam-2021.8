using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour, IInteractable
{
    [SerializeField] PlayerHealth playerHealth;

    public void PickUp()
    {
        playerHealth.Heal();
        gameObject.SetActive(false);
    }
}
