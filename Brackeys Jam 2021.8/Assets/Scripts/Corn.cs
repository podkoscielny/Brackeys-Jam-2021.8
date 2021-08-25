using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

public class Corn : MonoBehaviour, IInteractable
{
    public void PickUp()
    {
        GameManager.Instance.EatCorn();
        ObjectPooler.Instance.AddToPool(Tags.Corn, gameObject);
    }
}
