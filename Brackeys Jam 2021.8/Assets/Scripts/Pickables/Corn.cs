using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corn : Interactable
{
    [SerializeField] PoopSystem poopSystem;

    public override void PickUp()
    {
        poopSystem.EatCorn();
        gameObject.SetActive(false);
    }
}
