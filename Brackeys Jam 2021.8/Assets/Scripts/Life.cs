using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour, IInteractable
{
    private GameManager _gameManager;

    void Start() => _gameManager = GameManager.Instance;

    public void PickUp()
    {
        _gameManager.Heal();
        gameObject.SetActive(false);
    }
}
