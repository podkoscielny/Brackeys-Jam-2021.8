using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

public class Corn : MonoBehaviour, IInteractable
{
    private GameManager _gameManager;
    private ObjectPooler _objectPooler;

    void Awake()
    {
        _gameManager = GameManager.Instance;
        _objectPooler = ObjectPooler.Instance;
    }

    public void PickUp()
    {
        _gameManager.EatCorn();
        _objectPooler.AddToPool(Tags.Corn, gameObject);
    }
}
