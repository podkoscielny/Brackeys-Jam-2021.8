using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corn : MonoBehaviour, IInteractable
{
    private GameManager _gameManager;

    void Awake() => _gameManager = GameManager.Instance;

    public void PickUp()
    {
        _gameManager.EatCorn();
        gameObject.SetActive(false);
    }
}
