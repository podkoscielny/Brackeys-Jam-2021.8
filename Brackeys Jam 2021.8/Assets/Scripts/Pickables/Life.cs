using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : Interactable
{
    [SerializeField] PlayerHealth playerHealth;

    public override void PickUp()
    {
        playerHealth.Heal();
        gameObject.SetActive(false);
    }
}
