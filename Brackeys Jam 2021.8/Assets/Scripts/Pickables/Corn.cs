using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corn : MonoBehaviour, IInteractable
{
    [SerializeField] PoopSystem poopSystem;

    public void PickUp()
    {
        poopSystem.EatCorn();
        gameObject.SetActive(false);
    }
}
